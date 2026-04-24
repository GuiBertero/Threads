using System;
using System.IO;
using System.Threading;

class Program
{
    static string[] setores = {
        "Emergencia", "UTI", "Enfermaria", "Radiologia",
        "Laboratorio", "Farmacia", "CentroCirurgico", "Recepcao",
        "Cardiologia", "Neurologia", "Pediatria", "Oncologia",
        "Ortopedia", "Dermatologia", "Psiquiatria", "Nutricao",
        "Fisioterapia", "Administracao", "TI", "Seguranca"
    };

    static void Main()
    {
        string caminho = "hospital_sem_lock.txt";

        if (File.Exists(caminho))
            File.Delete(caminho);

        Thread[] threads = new Thread[20];

        for (int i = 0; i < 20; i++)
        {
            int id = i;

            threads[i] = new Thread(() =>
            {
                Random rnd = new Random();

                for (int j = 0; j < 200; j++)
                {
                    string log = $"{DateTime.Now:HH:mm:ss.fff} | {setores[id]} | Paciente {rnd.Next(1000, 9999)} | Evento {j}";

                    // ❌ SEM CONTROLE
                    File.AppendAllText(caminho, log + "\n");

                    Console.WriteLine(log);

                    Thread.Sleep(rnd.Next(5, 20));
                }
            });

            threads[i].Start();
        }

        foreach (var t in threads)
            t.Join();

        Console.WriteLine("\nFinalizado SEM controle.");
    }
}