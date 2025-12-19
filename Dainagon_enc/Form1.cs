using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Text; // Encoding を扱うために必要

namespace Dainagon_enc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // 初期値の設定
            txtCorpusName.Text = "OpenCHJ";
        }

        // 変換実行ボタンのクリックイベント
        private void btnConvert_Click(object sender, EventArgs e)
        {
           
        }




        // --- 変換ロジック（前回の内容を移植） ---
        private void ConvertTextToXml(string inputPath, string outputPath, string sampleID, string corpusName)
        {
            var lines = File.ReadAllLines(inputPath, Encoding.UTF8).ToList();
            var xmlOutput = new StringBuilder();

            xmlOutput.AppendLine($"<ocx:doc sampleID=\"{sampleID}\" corpusName=\"{corpusName}\" xmlns:ocx=\"https://www.ninjal.ac.jp/openCHJ/ns/structure/\">");

            int lineIndex = 0;
            if (lines.Count > 0) xmlOutput.AppendLine($"<title>{lines[lineIndex++]}</title><ocx:s/>");
            if (lines.Count > 1) xmlOutput.AppendLine($"<author>{lines[lineIndex++]}</author><ocx:s/>");

            // 本文開始位置を探す（青空文庫のラインを飛ばす処理などは適宜追加）
            int bodyStartIndex = lineIndex;
            for (int i = lineIndex; i < lines.Count; i++)
            {
                if (lines[i].Contains("-------")) { bodyStartIndex = i + 1; break; }
            }

            bool inParagraph = false;
            for (int i = bodyStartIndex; i < lines.Count; i++)
            {
                string line = lines[i].Trim();
                if (string.IsNullOrEmpty(line))
                {
                    if (inParagraph) { xmlOutput.AppendLine("</p>"); inParagraph = false; }
                    continue;
                }

                if (line.Contains("大見出し"))
                {
                    string h1 = Regex.Replace(line, @"［＃.*?］(.*?)［＃.*?］", "$1");
                    xmlOutput.AppendLine($"<h1>{ProcessInline(h1)}</h1><ocx:s/>");
                }
                else if (line.Contains("中見出し"))
                {
                    string h2 = Regex.Replace(line, @"［＃.*?］(.*?)［＃.*?］", "$1");
                    xmlOutput.AppendLine($"<h2>{ProcessInline(h2)}</h2><ocx:s/>");
                }
                else
                {
                    if (!inParagraph) { xmlOutput.Append("<p>"); inParagraph = true; }
                    xmlOutput.Append(ProcessInline(line));
                }
            }

            if (inParagraph) xmlOutput.AppendLine("</p>");
            xmlOutput.AppendLine("</ocx:doc>");

            File.WriteAllText(outputPath, xmlOutput.ToString(), Encoding.UTF8);
        }

        private string ProcessInline(string text)
        {
            // 特殊文字のエスケープ（XMLとして壊れないように）
            text = text.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");

            // ルビ・sタグ・speechタグの処理（前回と同様）
            text = Regex.Replace(text, @"｜([^《]+)《([^》]+)》", "<ocx:ruby rt=\"$2\">$1</ocx:ruby>");
            text = Regex.Replace(text, @"([々\u3400-\u4DBF\u4E00-\u9FFF\uF900-\uFAFF]+)《([^》]+)》", "<ocx:ruby rt=\"$2\">$1</ocx:ruby>");

            text = Regex.Replace(text, @"「([^」]+)」", match =>
            {
                string content = match.Groups[1].Value;
                content = Regex.Replace(content, @"([。！？])", "$1<ocx:s/>");
                return $"<speech>「{content}<ocx:s/>」</speech>";
            });

            text = Regex.Replace(text, @"(?<!<ocx:s/>)([。！？])", "$1<ocx:s/>");

            // エスケープしたタグを元に戻す（簡易的）
            return text.Replace("&lt;ocx:", "<ocx:").Replace("&lt;speech>", "<speech>").Replace("&lt;/speech>", "</speech>").Replace("&lt;s/&gt;", "<s/>").Replace("&lt;/ocx:", "</ocx:");
        }

        // ファイル選択ボタン
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "テキストファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtInputPath.Text = ofd.FileName;
                    // 出力先を自動生成（入力ファイルと同じ場所で拡張子をxmlに）
                    txtOutputPath.Text = Path.ChangeExtension(ofd.FileName, "xml");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string inputPath = txtInputPath.Text;
            string outputPath = txtOutputPath.Text;
            string sampleId = txtSampleId.Text;
            string corpusName = txtCorpusName.Text;

            // バリデーション
            if (string.IsNullOrEmpty(inputPath) || !File.Exists(inputPath))
            {
                MessageBox.Show("有効な入力ファイルを選択してください。");
                return;
            }

            try
            {
                // --- ここで文字コードチェックと変換を実行 ---
                ConvertToUtf8BomIfSjis(inputPath);

                ConvertTextToXml(inputPath, outputPath, sampleId, corpusName);
                MessageBox.Show("変換が完了しました！\n" + outputPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました:\n" + ex.Message);
            }
        }

        /// <summary>
        /// ファイルの文字コードを確認し、SJISであればUTF-8(BOM付き)に変換して上書きします。
        /// </summary>
        private void ConvertToUtf8BomIfSjis(string filePath)
        {
            // .NET Core/5+ で Shift-JIS を扱うために必要な登録
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding sjis = Encoding.GetEncoding("shift_jis");
            Encoding utf8Bom = new UTF8Encoding(true);

            byte[] fileData = File.ReadAllBytes(filePath);

            // BOM (Byte Order Mark) のチェック
            // UTF-8 BOM は 0xEF, 0xBB, 0xBF
            if (fileData.Length >= 3 && fileData[0] == 0xEF && fileData[1] == 0xBB && fileData[2] == 0xBF)
            {
                // すでに UTF-8 (BOMあり) なので何もしない
                return;
            }

            // ここでは簡易的に「BOMがない場合はSJIS」として読み込みを試みます
            // (より厳密な判定が必要な場合は外部ライブラリ等が必要ですが、通常はこれで十分です)
            try
            {
                string text = File.ReadAllText(filePath, sjis);

                // UTF-8 (BOM付き) で上書き保存
                File.WriteAllText(filePath, text, utf8Bom);
            }
            catch (Exception ex)
            {
                throw new Exception("文字コードの変換中にエラーが発生しました: " + ex.Message);
            }
        }
    }
}