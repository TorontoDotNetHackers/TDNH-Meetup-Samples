using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace PipelineFun
{
    public partial class frmMain : Form
    {
        //This holds the data that will be transformed in the iterations through the DLLs created by the rest of the group
        private string data;

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnRunPipeline_Click(object sender, EventArgs e)
        {
            //Retrieve initial input from txtInput. If none exists, fail and tell the user initial data is required
            data = txtInput.Text;
            if (string.IsNullOrEmpty(data))
            {
                lblInfo.Text = "Please provide initial text to transform.";
                return;
            }

            //Loop through all the DLLs found in the folder where the EXE lives and pass these to the TransformData method to
            //perform the reflection and run the Transform method
            DirectoryInfo binFolder = new DirectoryInfo(Environment.CurrentDirectory);
            foreach (FileInfo dllFile in binFolder.GetFiles("*.dll")) data = TransformData(dllFile.FullName, data);

            //Place the finished transformed data in the txtOutput textbox
            txtOutput.Text = data;
        }

        static string TransformData(string path, string data)
        {
            //This is the type we are looking for within the DLLs provided by the rest of the group
            Type transformType = typeof (TDNH.IInformation);
            
            //Load the current DLL into memory
            Assembly dll = Assembly.LoadFile(path);

            //Get an array of all types (classes) that exist in the current DLL
            Type[] dllTypes = dll.GetTypes();

            //Loop through the type array
            foreach (Type dllType in dllTypes)
            {
                //If the current type implements the type we are looking for then go to work
               if (transformType.IsAssignableFrom(dllType) && dllType != typeof(TDNH.IInformation))
               {
                   //Dynamically create an instance of the class
                   //Note: The class created is weakly typed (ie. is simply a generic Object) so we must cast it appropriately before using it
                   TDNH.IInformation instance = (TDNH.IInformation) Activator.CreateInstance(dllType);

                   //Now call the Transform method 
                   //(which we know exists in this class as this is the one method that was in the interface all these classes are implementing)
                   data = instance.Transform(data);
               }
            }

            //Finally return the transformed data
            return data;
        }
 
    }
}
