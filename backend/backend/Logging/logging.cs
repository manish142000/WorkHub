namespace backend.Logging
{
    public class logging : Ilogging
    {
        public void Log(string message, string type)
        {
            if( type == "Error")
            {
                Console.WriteLine("Error-> " + message);
            }
            else
            {
                Console.WriteLine(message);
            }
        }
    }
}
