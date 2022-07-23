using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Radagon.RevitUtilities
{
    internal class ProjectParameterManager
    {
        public IList<ProjectParameter> parameters = new List<ProjectParameter>();

        /// <summary>
        /// Holds the data for a project parameter
        /// </summary>
        public class ProjectParameter
        {
            public Definition Definition = null;
            public ElementBinding Binding = null;
            public string Name = null;                // Needed because accsessing the Definition later may produce an error.
        }

        public ProjectParameterManager(Document document)
        {
            initProjectParameters(document);
        }

        /// <summary>
        /// Returns a list of the objects containing references to the project parameter definitions
        /// </summary>
        /// <param name="doc">The project document being quereied</param>
        /// <returns>A list of type ProjectParameter</returns>
        private void initProjectParameters(Document Document)
        {
            // Following good SOA practices, first validate incoming parameters

            if (Document == null)
                throw new ArgumentNullException("doc");

            if (Document.IsFamilyDocument)
                throw new Exception("doc can not be a family document.");

            List<ProjectParameter> result = new List<ProjectParameter>();

            BindingMap map = Document.ParameterBindings;
            DefinitionBindingMapIterator it = map.ForwardIterator();
            it.Reset();
            while (it.MoveNext())
            {
                ProjectParameter newProjectParameterData = new ProjectParameter();

                newProjectParameterData.Definition = it.Key;
                newProjectParameterData.Name = it.Key.Name;
                newProjectParameterData.Binding = it.Current as ElementBinding;

                result.Add(newProjectParameterData);
            }
            parameters = result;
        }
    }
}
