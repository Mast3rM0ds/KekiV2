using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using KekiEditor;
using KekiEditor.Utilities;

namespace KekiEditor.GameProject
{
    public class ProjectTemplate
    {
        [DataMember]
        public string ProjectType { get; set; }
        [DataMember]
        public string ProjectFile { get; set; }
        [DataMember]
        public List<string> Folders { get; set; }

        public byte[] Icon { get; set; }
        public byte[] Screenshot { get; set; }
        public string IconFilePath { get; set; }
        public string ScreenshotFilePath { get; set; }
        public string ProjectFilePath { get; set; }
    }

    public class NewProject : BaseViewModel
    {
        //todo: get the damn path from the installer so yeah the templates are there
        private readonly string _templatePath = @"..\..\KekiEditor\ProjectTemplates";
        private string name;
        private string path;

        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Path
        {
            get => path;
            set
            {
                if (path != value)
                {
                    path = value;
                    OnPropertyChanged(nameof(Path));
                }
            }
        }

        private ObservableCollection<ProjectTemplate> projectTemplates = new ObservableCollection<ProjectTemplate>();
        public ReadOnlyObservableCollection<ProjectTemplate> ProjectTemplates { get; }

        public NewProject()
        {
            ProjectTemplates = new ReadOnlyObservableCollection<ProjectTemplate>(projectTemplates);

            Name = "Default Keki Project";
            Path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\KekiProjects";
            try
            {
                var templates = Directory.GetFiles(_templatePath, "template.xml", SearchOption.AllDirectories);
                Debug.Assert(templates.Length != 0);
                foreach (var template in templates)
                {
                    var tep = Serializer.FromFile<ProjectTemplate>(template);
                    tep.IconFilePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(template), "Icon.png"));
                    tep.Icon = File.ReadAllBytes(tep.IconFilePath);
                    tep.ScreenshotFilePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(template), "Screenshot.png"));
                    tep.Screenshot = File.ReadAllBytes(tep.ScreenshotFilePath);
                    tep.ProjectFilePath = System.IO.Path.GetFullPath(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(template), tep.ProjectFile));
                    
                    projectTemplates.Add(tep);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                //too poo: log the error like a brit
            }
        }
    }
}
