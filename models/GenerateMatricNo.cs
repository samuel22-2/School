namespace WebApplication3.models
{
    public class GenerateMatricNo
    {
        public static string GenerateMatricN(int lastId)
        {
            string MatricNo= string.Empty;
            var prefix = string.Empty;
            //get the current year
            string yearcode = DateTime.Now.Year.ToString();
            //get the length of the year 
            int length=yearcode.Length;
            int num = length - 2;
            int length2 = length - num;
            //get the last 2 part of the year e.g. 23 in 2023
            yearcode = yearcode.Substring(num, length2);  
            string strlastId = lastId.ToString();
            length= strlastId.Length;
            num = 4 - length;
            
            if(num> 0)
            {
                //appending the prefix of 0s to complete the length of fours
                for(var i = 0; i < num; i++) 
                {
                    prefix += "0";
                }    

               

           }
             MatricNo = $"{yearcode}/{prefix}{lastId}";
            return MatricNo;
        }
    }
}
