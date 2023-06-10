namespace WebApplication3.models
{
    public class Admission
    {
        public static double SendScore(string waecGrade)
        {
          double waecScore = 0;
            if (waecGrade=="A1")
            {
                waecScore = 5.0;
                //double m = 5.0;
                //m  = Convert.ToDouble(waecScore);
            }
            else if (waecGrade == "B2")
            {
                waecScore = 4.0;
                //double n = 4.0;
                //n = Convert.ToDouble(waecScore);
            }

            else if (waecGrade == "B3")
            {
                waecScore = 3.5;
            }

            else if (waecGrade == "C4")
            {
                waecScore = 3.0;
            }

            else if (waecGrade == "C5")
            {
                waecScore = 2.5;
            }

            else if (waecGrade == "C6")
            {
                waecScore = 2.0;
            }

            else if (waecGrade == "D7")
            {
                waecScore = 1.5;
            }

            else if (waecGrade == "E8")
            {
                waecScore = 1.0;
            }

            else if (waecGrade == "F9")
            {
                waecScore = 0.0;
            }

            return waecScore;
        }

        //public static string SendJamb(string JambScore)
        //{

        //}
    }
}
