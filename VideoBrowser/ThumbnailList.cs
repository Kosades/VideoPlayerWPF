using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.Collections.Specialized;
using System.Windows.Controls;

namespace VideoBrowser
{
    public class ThumbnailList : ObservableCollection<String>
    {
        // Can't set the path in the constructor, 
        // because the main form uses static binding to 
        // bind to an instance of this class, which gets
        // created before the form (and therefore, before 
        // you've specified a folder). If you create a new 
        // instance of this class when you supply the path
        // name, the static binding is now binding to the original
        // (empty) collection. Therefore, this code must
        // allow you to modify the folder for the existing
        // instance of this class.
        String _folderName;

        public string FolderName
        {
            get
            {
                return _folderName;
            }

            set
            {
                _folderName = value;

                // Now fill in the collection of 
                // file names:
                this.Clear();
                foreach (string fileName in
                  Directory.GetFiles(this.FolderName, "*.jpg"))
                {
                    this.Add(fileName);
                }
            }
        }
    }
}