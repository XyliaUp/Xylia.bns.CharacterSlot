using System;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace CharacterSlot
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			//标题文本
			//this.Text = $"{ this.Text } ({ System.Reflection.Assembly.GetExecutingAssembly().ImageRuntimeVersion })";
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			for (int i = 1; i <= 20; i++) this.comboBox1.Items.Add(i.ToString());

			this.comboBox1.SelectedIndex = 0;
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			var Configuration = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\BnS\TENCENT\ClientConfiguration.xml";
			if (!File.Exists(Configuration))
			{
				label2.Text = "请先登录一次游戏";
				return;
			}


			var XmlDoc = new XmlDocument();
			XmlDoc.Load(Configuration);

			var SlotOrder = XmlDoc.SelectSingleNode($@"config/option[@name='character-slot-order-{this.comboBox1.Text}']");
			if(SlotOrder is null)
			{
				label2.Text = "参数无效";
				return;
			}


			if(!int.TryParse(SlotOrder.Attributes["value"]?.Value,out int result) || result == 0)
			{
				label2.Text = "无效角色编号";
				return;
			}


			if (true && result > 50000)
			{
				label2.Text = "非体验服角色，请先登录体验服后再试";
				return;
			}

			label2.Text =$"当前角色编号: {result}";
		}
	}
}
