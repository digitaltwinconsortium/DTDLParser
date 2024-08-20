namespace MyParser
{
    using DTDLParser;

    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var parser = new ModelParser();
                Console.WriteLine("ModelParser initialized correctly");
            }
            catch (Exception ex)
            {
                foreach (var e in ((ParsingException)(ex?.InnerException)).Errors)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
