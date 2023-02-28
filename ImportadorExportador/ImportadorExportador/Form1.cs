using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ImportadorExportador
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            if (File.Exists(txtArquivoImportar.Text) == false)
            {
                MessageBox.Show("Arquivo informado não foi encontrado!", "Erro");
                return;
            }

            /*
            [
	            {
		            "id": 10,
		            "codEstabel": "2",
		            "NrPedCli": "200500",
		            "itens": [{
				            "seq": 10,
				            "codItem": "ATL1020",
				            "qtde": 10,
				            "precoUnit": 10.50,
				            "total": 105
			            }, {
				            "seq": 20,
				            "codItem": "ATL2580",
				            "qtde": 100,
				            "precoUnit": 2.50,
				            "total": 250
			            }
		            ]
	            }
            ] */

            String conteudoArquivo = File.ReadAllText(txtArquivoImportar.Text);

            MessageBox.Show(conteudoArquivo);

            JArray jArrayPedidos = JArray.Parse(conteudoArquivo);
            JArray jArrayItens;

            foreach (JObject jObjectPedido in jArrayPedidos)
            {
                MessageBox.Show(jObjectPedido.GetValue("id").ToString());
                if (jObjectPedido.ContainsKey("itens"))
                {
                    jArrayItens = JArray.FromObject(jObjectPedido.GetValue("itens"));

                    foreach (JObject jObjectItem in jArrayItens)
                    {
                        MessageBox.Show(getValor(jObjectItem,"seq") + Environment.NewLine +
                                        getValor(jObjectItem,"codItem") + Environment.NewLine +
                                        getValor(jObjectItem,"qtde") + Environment.NewLine +
                                        getValor(jObjectItem,"precoUnit") + Environment.NewLine +
                                        getValor(jObjectItem,"total"));
                    }
                }
            }
        }

        private object getValor(JObject jObjeto,String campo)
        {
            if (jObjeto.ContainsKey(campo))
            {
                return jObjeto.GetValue(campo);
            }
            else
            {
                return "";
            }
        }
    }
}
