using DCA.DataAccess;
using DCA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCA.ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            //using (var reciversRepository = new ReciversRepository())
            //{
            //    reciversRepository.Add(new Reciver { FullName = "Petya", Address = "Astana"});
            //}

            //using (var mailsRepository = new MailsRepository())
            //{
            //    Guid id = new Guid("866ed8c3-05b5-4776-a3ca-86120c38585c");
            //    mailsRepository.Delete(id);

            //    var mail = mailsRepository.GetAll().ToList().FirstOrDefault();
            //    mail.Text = "danil";
            //    mailsRepository.Update(mail);
            //}
            SendMail();
            //Console.ReadLine();
            //AddReciver();
        }

        static void SendMail()
        {
            Console.WriteLine("Кому отправить сообщение?");
            using (var reciversRepository = new ReciversRepository())
            using (var mailsRepository = new MailsRepository())
            {
                Mail mail = new Mail();
                int ineration = 1;
                var recivers = reciversRepository.GetAll();

                foreach (var reciver in recivers)
                {
                    Console.WriteLine(ineration + ": " + reciver.FullName);
                }

                int reciverNumber;

                if (int.TryParse(Console.ReadLine(), out reciverNumber) && recivers.Count >= reciverNumber)
                {
                    mail.Reciver = recivers.ElementAt(reciverNumber - 1);
                    mail.ReciverId = recivers.ElementAt(reciverNumber - 1).Id;
                }
                else return;

                Console.WriteLine("Введите тему сообщения");
                mail.Theme = Console.ReadLine();
                Console.WriteLine("Введите сообщениe");
                mail.Text = Console.ReadLine();

                mailsRepository.Add(mail);
            }
        }

        static void AddReciver()
        {
            using (var reciversRepository = new ReciversRepository())
            {
                Reciver reciver = new Reciver();
                Console.WriteLine("Введите имя");
                reciver.FullName = Console.ReadLine();
                Console.WriteLine("Введите адрес");
                reciver.Address = Console.ReadLine();

                reciversRepository.Add(reciver);
            }
        }
    }
}
