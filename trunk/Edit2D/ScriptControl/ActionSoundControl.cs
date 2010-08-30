using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Edit2DEngine.Actions;
using System.IO;
using Edit2D.UC;

namespace Edit2D.ScriptControl
{
    public partial class ActionSoundControl : UserControlLocal
    {
        public ActionSound ActionSound { get; set; }

        public ActionSoundControl()
        {
            InitializeComponent();
        }

        #region Evènements
        private void listboxSounds_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateActionsound();
        } 
        #endregion

        #region Méthodes privées
        private void InitListBoxSounds()
        {
            listboxSounds.Items.Clear();

            string[] files = Directory.GetFiles(Environment.CurrentDirectory + @"\Data\Sounds\", "*.WAV;*.MP3;*.OGG");

            foreach (String fileName in files)
            {
                FileInfo fileInfo = new FileInfo(fileName);
                listboxSounds.Items.Add(fileInfo.Name.Remove(fileInfo.Name.Length - fileInfo.Extension.Length));
            }
        }

        private void UpdateActionsound()
        {
            if (listboxSounds.SelectedIndex != -1)
            {
                ActionSound.SoundName = listboxSounds.SelectedItem.ToString();
            }
            else
            {
                ActionSound.SoundName = String.Empty;
            }
        } 
        #endregion
    }
}
