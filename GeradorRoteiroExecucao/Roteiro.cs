using System.Numerics;

namespace GeradorRoteiroExecucao
{
    public class Roteiro
    {
        public int Id { get; set; }
        public string? Arquivo { get; set; }
        public string? Host { get; set; }
        public string? Schema { get; set; }

        //sobrescrevendo o método to string, que herda de System
        //public override string ToString()
        //{
        //    return $"{Id}, {Arquivo}, {Host}, {Schema}";

        //    ////object
        //    //var roteiro = new Roteiro("1", "abc.csv", "HOST", "SCHEMA");
        //    //Console.WriteLine(roteiro);

        //    ////string
        //    //var rot = "1 abc.csv host schema";
        //    //roteiro = rot;

        //    //Console.WriteLine(rot);
        //}

        public static implicit operator string(Roteiro roteiro) => $"{roteiro.Id},{roteiro.Arquivo},{roteiro.Host},{roteiro.Schema}";

    }


}
