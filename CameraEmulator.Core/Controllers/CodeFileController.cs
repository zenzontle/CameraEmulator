using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CameraEmulator.Core.Scanners;

namespace CameraEmulator.Core.Controllers
{
    public class CodeFileController
    {
        private readonly CameraController _cameraController;
        private readonly int _codeDelay;

        private static readonly EventWaitHandle WaitHandle = new ManualResetEvent(true);

        public CodeFileController(Scanner caseScanner, Scanner sleeveScanner, Scanner itemScanner, int codeDelay)
        {
            _cameraController = new CameraController(caseScanner, sleeveScanner, itemScanner);
            _codeDelay = codeDelay;
        }

        public async Task SendItemsFile(string caseCode, int sleevesPerCase, int itemsPerSleeve, string fileName, IProgress<CodeProgressInfo> progress)
        {
            try
            {
                _cameraController.SendCaseCode(caseCode);

                var lines = File.ReadLines(fileName).Take(sleevesPerCase * itemsPerSleeve).ToList();
                for (int i = 0; i < lines.Count; i++)
                {
                    Task wait = Task.Delay(_codeDelay);
                    string code = GetCodeFromLine(lines[i]);
                    _cameraController.SendItemCode(code);
                    await wait;

                    if ((i + 1) % itemsPerSleeve == 0)
                    {
                        //Send upper sleeve
                        wait = Task.Delay(1000);
                        code = GetCodeFromLine(lines[i + 1 - itemsPerSleeve]);
                        _cameraController.SendSleeveCode(code);
                        await wait;

                        //Send lower sleeve
                        wait = Task.Delay(1000);
                        code = GetCodeFromLine(lines[i]);
                        _cameraController.SendSleeveCode(code);
                        await wait;
                    }

                    progress.Report(new CodeProgressInfo {CodesSent = i + 1, LastCodeSent = code});
                    WaitHandle.WaitOne();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                _cameraController.DisconnectAll();
            }
        }

        private string GetCodeFromLine(string fileLine)
        {
            return fileLine.Substring(0, fileLine.IndexOf(','));
        }

        public void Pause()
        {
            WaitHandle.Reset();
        }

        public void Resume()
        {
            WaitHandle.Set();
        }
    }
}
