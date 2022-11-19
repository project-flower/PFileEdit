using System;
using System.IO;
using System.Windows.Forms;

namespace PFileEdit
{
    public partial class FormMain : Form
    {
        #region Private Fields

        private bool dirty = false;
        private bool pathChanging = false;
        private string prevPath = string.Empty;

        #endregion

        #region Public Methods

        public FormMain()
        {
            InitializeComponent();
            MinimumSize = Size;
        }

        #endregion

        #region Private Methods

        private void LoadFileInfo(string fileName)
        {
            dirty = false;

            if (!(Directory.Exists(fileName) || File.Exists(fileName)))
            {
                propertyGrid.SelectedObject = null;
                prevPath = string.Empty;
                return;
            }

            prevPath = fileName;
            propertyGrid.SelectedObject = new Elements(fileName);
        }

        private void ShowErrorMessage(string message)
        {
            ShowMessage(message, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private DialogResult ShowMessage(string text, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            return MessageBox.Show(this, text, Text, buttons, icon);
        }

        #endregion

        // Designer's Methods

        private void buttonApply_Click(object sender, EventArgs e)
        {
            if (!(propertyGrid.SelectedObject is Elements elements)) return;

            try
            {
                FileSystemModifier.Update(elements);
                LoadFileInfo(comboBoxPath.Text);
            }
            catch (Exception exception)
            {
                ShowErrorMessage(exception.Message);
            }
        }

        private void buttonRevert_Click(object sender, EventArgs e)
        {
            //propertyGrid.ResetSelectedProperty();
            LoadFileInfo(comboBoxPath.Text);
        }

        private void comboBoxPath_TextChanged(object sender, EventArgs e)
        {
            if (pathChanging) return;

            if (dirty)
            {
                if (ShowMessage(
                    "Values are not applied.\r\nAre you sure you want to discard the changes?"
                    , MessageBoxButtons.OKCancel
                    , MessageBoxIcon.Warning)
                    != DialogResult.OK)
                {
                    pathChanging = true;
                    comboBoxPath.Text = prevPath;
                    pathChanging = false;
                    return;
                }
            }

            try
            {
                LoadFileInfo(comboBoxPath.Text);
            }
            catch (Exception exception)
            {
                ShowErrorMessage(exception.Message);
                return;
            }
        }

        private void dragEnter(object sender, DragEventArgs e)
        {
            e.Effect
                = (e.Data.GetDataPresent(DataFormats.FileDrop)
                ? DragDropEffects.All
                : DragDropEffects.None);
        }

        private void dragDrop(object sender, DragEventArgs e)
        {
            if (!(e.Data.GetData(DataFormats.FileDrop) is string[] dropData) || (dropData.Length < 1))
            {
                return;
            }

            comboBoxPath.Text = dropData[0];
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            dirty = true;
        }

        private void shown(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 1)
            {
                comboBoxPath.Text = args[1];
            }
        }
    }
}
