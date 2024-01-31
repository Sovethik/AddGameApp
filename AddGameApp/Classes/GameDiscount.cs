using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddGameApp
{
    public partial class Games
    {

        public string NameGameAndDeveloper
        {
            get
            {
                string p;
                p = Convert.ToString(name) + Convert.ToString(Developers.name);
                return p;
            }
            set
            {

            }
        }

        public decimal PirceAndDiscount
        {
            get
            {
                decimal p;
                if (discount == null)
                    p = price;
                else
                    p = Convert.ToDecimal(DoubleDiscount);

                return p;
            }
            set
            {

            }
        }

        public string StrDiscount
        {
            get
            {
                string f = "";
                if (discount != null)
                {
                    f += Convert.ToString(Convert.ToDouble(price) - Convert.ToDouble(price) * discount);
                    return f;
                }
                else
                {
                    return "";
                }
            }
            set
            {

            }
        }
        public double DoubleDiscount
        {
            get
            {
                return Convert.ToDouble(price) - Convert.ToDouble(price) * Convert.ToDouble(discount);
            }
            set
            {

            }
        }
        public string BoxDiscount
        {
            get
            {

                if (discount != null)
                {
                    string f = "-";
                    return f += Convert.ToString(Convert.ToDouble(discount * 100)) + "%";
                }
                else
                    return "";
                
            }
            set
            {

            }
        }
    }
}
