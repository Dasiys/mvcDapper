using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Common
{
    public class VerifyCodeTools
    {
        /// <summary>
        /// 设置或获取验证码长度
        /// </summary>
        public int Length { set; get; } = 4;

        /// <summary>
        /// 创建验证码
        /// </summary>
        /// <returns></returns>
        public string CreateVertifyCode()
        {
            string str = string.Empty;
            Random random = new Random();
            while (str.Length < Length)
            {
                char ch;
                int num = random.Next();
                if ((num % 3) == 0)
                {
                    ch = (char)(0x61 + ((ushort)(num % 0x1a)));
                }
                else
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                if ((((ch != '0') && (ch != 'o')) && ((ch != '1') && (ch != '7'))) && (ch != 'l'))
                {
                    str = str + ch.ToString();
                }
            }
            return str;
        }

        /// <summary>
        /// 创建验证码的图片
        /// </summary>
        /// <param name="validateCode">验证码内容</param>
        /// <returns></returns>
        public byte[] CreateValidateGraphic(string validateCode)
        {
            int maxValue = 0x2d;
            int width = validateCode.Length * 20;
            Bitmap image = new Bitmap(width, 30);
            Graphics graphics = Graphics.FromImage(image);
            try
            {
                graphics.Clear(Color.AliceBlue);
                graphics.DrawRectangle(new Pen(Color.Black, 0f), 0, 0, image.Width - 1, image.Height - 1);
                Random random = new Random();
                Pen pen = new Pen(Color.LightGray, 0f);
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(0, image.Width);
                    int y = random.Next(0, image.Height);
                    graphics.DrawRectangle(pen, x, y, 1, 1);
                }
                char[] chArray = validateCode.ToCharArray();
                StringFormat format = new StringFormat(StringFormatFlags.NoClip);
                format.Alignment = StringAlignment.Center;
                format.LineAlignment = StringAlignment.Center;
                Color[] colorArray = new Color[] { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
                for (int j = 0; j < chArray.Length; j++)
                {
                    int index = random.Next(7);
                    random.Next(4);
                    Font font = new Font("Comic Sans MS", 14f, FontStyle.Underline);
                    Brush brush = new SolidBrush(colorArray[index]);
                    Point point = new Point(14, 11);
                    float angle = random.Next(-maxValue, maxValue);
                    graphics.TranslateTransform((float)point.X, (float)point.Y);
                    graphics.RotateTransform(angle);
                    graphics.DrawString(chArray[j].ToString(), font, brush, 1f, 1f, format);
                    graphics.RotateTransform(-angle);
                    graphics.TranslateTransform(2f, (float)-point.Y);
                }
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                return stream.ToArray();
            }
            finally
            {
                graphics.Dispose();
                image.Dispose();
            }
        }
    }
}
