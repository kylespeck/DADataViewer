using System;
using System.Windows.Forms;

namespace DADataViewer
{
    public partial class MainForm : Form
    {
        private ItemsForm _itemsForm;
        private SkillsForm _skillsForm;
        private SkillsForm _spellsForm;
        private MonstersForm _monstersForm;
        private MotionsForm _motionsForm;
        private EffectsForm _effectsForm;
        private SoundsForm _soundsForm;

        public MainForm()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_itemsForm == null)
            {
                _itemsForm = new ItemsForm();
                _itemsForm.MdiParent = this;
                _itemsForm.FormClosing += ChildFormClosing;
            }
            _itemsForm.Visible = !_itemsForm.Visible;
        }

        private void skillsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_skillsForm == null)
            {
                _skillsForm = new SkillsForm("Skills", "skill001.epf");
                _skillsForm.MdiParent = this;
                _skillsForm.FormClosing += ChildFormClosing;
            }
            _skillsForm.Visible = !_skillsForm.Visible;
        }

        private void spellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_spellsForm == null)
            {
                _spellsForm = new SkillsForm("Spells", "spell001.epf");
                _spellsForm.MdiParent = this;
                _spellsForm.FormClosing += ChildFormClosing;
            }
            _spellsForm.Visible = !_spellsForm.Visible;
        }

        private void monstersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_monstersForm == null)
            {
                _monstersForm = new MonstersForm();
                _monstersForm.MdiParent = this;
                _monstersForm.FormClosing += ChildFormClosing;
            }
            _monstersForm.Visible = !_monstersForm.Visible;
        }

        private void motionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_motionsForm == null)
            {
                _motionsForm = new MotionsForm();
                _motionsForm.MdiParent = this;
                _motionsForm.FormClosing += ChildFormClosing;
            }
            _motionsForm.Visible = !_motionsForm.Visible;
        }

        private void effectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_effectsForm == null)
            {
                _effectsForm = new EffectsForm();
                _effectsForm.MdiParent = this;
                _effectsForm.FormClosing += ChildFormClosing;
            }
            _effectsForm.Visible = !_effectsForm.Visible;
        }

        private void soundsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_soundsForm == null)
            {
                _soundsForm = new SoundsForm();
                _soundsForm.MdiParent = this;
                _soundsForm.FormClosing += ChildFormClosing;
            }
            _soundsForm.Visible = !_soundsForm.Visible;
        }

        private void ChildFormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                ((Form)sender).Visible = false;
                e.Cancel = true;
            }
        }
    }
}
