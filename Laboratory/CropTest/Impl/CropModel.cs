
using System.Drawing;
namespace Laboratory.CropTest
{
    public class CropModel
    {
        private Size _size;

        public Size Size
        {
            get
            {
                if (_size == null || _size == Size.Empty)
                {
                    _size = new Size(320, 320);
                }
                return _size;
            }
            set { _size = value; }
        }
    }
}
