using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
namespace EPoller
{
    public partial class QRCode : Form
    {
        String password;
        public QRCode(String pass)
        {
            InitializeComponent();
            password = pass;
        }

        private void QRCode_Load(object sender, EventArgs e)
        {
            QRCodeEncoder encoder = new QRCodeEncoder();
            Bitmap qrcode = encoder.Encode(password);
            qrimage.Image = qrcode as Image;
        }
    }
}
