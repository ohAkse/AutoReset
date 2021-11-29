using System;
using System.Threading;

namespace AutoReset
{
    class Program
    {
        private static AutoResetEvent workEvent = new AutoResetEvent(false);
        private static AutoResetEvent mainEvent = new AutoResetEvent(false);


        static private void Process(int i)
        {
            Console.WriteLine("메인스레드 5초동안 Sleep 및 새로운 스레드에서 시그널 보냄");
           Thread.Sleep(TimeSpan.FromSeconds(5));
            workEvent.Set(); //Process를 돌리는 스레드에서 준비가 신호를 날림 -> Main Thread에서 WaitOne하여 대기타야함
            Console.WriteLine("신호 받고 메인스레드는 살아나며 메인스레드에서 시그널 받을 설정 완료");
            mainEvent.WaitOne();
            Console.WriteLine("메인 스레드에서 시그널 받음");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            Console.WriteLine("workevent를 다시 set해줌 -> 메인 스레드 차단 및 새로운 스레드에서 시그널 다시 보냄");
            workEvent.Set();


        }
        static void Main(string[] args)
        {

            Thread t = new Thread(() => Process(5));
            t.Start();

            Console.WriteLine("메인스레드에서 새로 생성한 스레드의 시그널을 기다림");
            workEvent.WaitOne();
            Thread.Sleep(TimeSpan.FromSeconds(5));
            Console.WriteLine("메인 스레드에서 5초 대기 후 시그널 보냄");
            mainEvent.Set();
            Console.WriteLine("dd메인스레드에서 새로 생성한 스레드의 시그널을 다시 기다림");
            Console.WriteLine("DDDDasdsad");
            workEvent.WaitOne();
            
        }
    }
}
