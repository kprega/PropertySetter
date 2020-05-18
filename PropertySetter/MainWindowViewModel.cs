namespace PropertySetter
{
    using Fusion;
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using Tekla.Structures;
    using Tekla.Structures.Model;

    /// <summary>
    /// MainWindow view model.
    /// </summary>
    public class MainWindowViewModel : WindowViewModel
    {
        #region Constants

        private const string FILTER_FILE_EXTENSION = "SObjGrp";
        private const string SETTING_EXTENSION = "PSetXml";
        private const string LOG_FILENAME = "PropertySetter.log";

        #endregion

        #region Fields

        /// <summary>
        /// Dictionary storing relation between Tekla types and property mappings.
        /// </summary>
        private Dictionary<Type, IMapping> typeMap = new Dictionary<Type, IMapping>()
        {
            {typeof(ModelObject),    new ModelObjectMapping()    },
            {typeof(Assembly),       new AssemblyMapping()       },
            {typeof(Part),           new PartMapping()           },
            {typeof(Beam),           new BeamMapping()           },
            {typeof(BooleanPart),    new BooleanPartMapping()    },
            {typeof(Reinforcement),  new ReinforcementMapping()  },
            {typeof(BaseRebarGroup), new BaseRebarGroupMapping() },
            {typeof(SingleRebar),    new SingleRebarMapping()    }
        };

        private bool isConnected;
        private int selectedFilterIndex;
        private int selectedRuleIndex;
        private string attributeName;
        private string attributeValue;
        private string modelName;
        private string newSetting;
        private string selectedSetting;
        private FileSystemWatcher[] filterWatchers;
        private FileSystemWatcher[] settingWatchers;
        private List<string> filters;
        private List<string> settings;
        private Model model;
        private TeklaStructuresFiles tsFiles;
        private XmlSerializer serializer;
        private ObservableCollection<Rule> rules;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a boolean value indicating whether the application is connected.
        /// </summary>
        public bool IsConnected { get => isConnected; private set => SetValue(ref isConnected, value); }

        /// <summary>
        /// Stores index of filter selected from the list.
        /// </summary>
        public int SelectedFilterIndex { get => selectedFilterIndex; set => SetValue(ref selectedFilterIndex, value); }

        /// <summary>
        /// Stores index of currently selected rule.
        /// </summary>
        public int SelectedRuleIndex { get => selectedRuleIndex; set => SetValue(ref selectedRuleIndex, value); }

        /// <summary>
        /// Stores attribute name of currently prepared rule.
        /// </summary>
        public string AttributeName { get => attributeName; set => SetValue(ref attributeName, value); }

        /// <summary>
        /// Stores attribute value of currently prepared rule.
        /// </summary>
        public string AttributeValue { get => attributeValue; set => SetValue(ref attributeValue, value); }

        /// <summary>
        /// Gets the model name.
        /// </summary>
        public string ModelName { get => modelName; private set => SetValue(ref modelName, value); }

        /// <summary>
        /// Stores name of selected setting.
        /// </summary>
        public string SelectedSetting { get => selectedSetting; set => SetValue(ref selectedSetting, value); }

        /// <summary>
        /// Stores name of user-typed setting.
        /// </summary>
        public string NewSetting { get => newSetting; set => SetValue(ref newSetting, value); }

        /// <summary>
        /// Log property.
        /// </summary>
        public static ILog Log { get => LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); }

        /// <summary>
        /// Property for handling filters collection.
        /// </summary>
        public List<string> Filters { get => filters; set => SetValue(ref filters, value); }

        /// <summary>
        /// Property for handling settings collection.
        /// </summary>
        public List<string> Settings { get => settings; set => SetValue(ref settings, value); }

        /// <summary>
        /// Property for handling rules collection.
        /// </summary>
        public ObservableCollection<Rule> Rules { get => rules; set => SetValue(ref rules, value); }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        protected override async void Initialize()
        {
            base.Initialize();

            this.IsConnected = await System.Threading.Tasks.Task.Run(() => TeklaStructures.Connect());

            if (IsConnected)
            {
                model = new Model();
                ModelName = model.GetInfo().ModelName;
                serializer = new XmlSerializer(typeof(ObservableCollection<Rule>));
                Rules = new ObservableCollection<Rule>();
                ConfigureFileSystemWatchers();
                ConfigureLog();
            }
        }

        /// <summary>
        /// Configures log for further use.
        /// </summary>
        private void ConfigureLog()
        {
            var logDirectory = model.GetInfo().ModelPath != string.Empty ? model.GetInfo().ModelPath + "\\logs" : Path.GetTempPath();
            GlobalContext.Properties["LogName"] = logDirectory + "\\" + LOG_FILENAME;
            log4net.Config.XmlConfigurator.Configure();
        }

        /// <summary>
        /// Gets inheritance hierarchy for given type.
        /// </summary>
        /// <param name="type">Type of object.</param>
        /// <returns>Hierarchy of types.</returns>
        private IEnumerable<Type> GetInheritanceHierarchy(Type type)
        {
            for (var current = type; current != null; current = current.BaseType)
                yield return current;
        }

        /// <summary>
        /// Enables monitoring over available settings and filter files.
        /// </summary>
        private void ConfigureFileSystemWatchers()
        {
            tsFiles = new TeklaStructuresFiles();
            // Add current model folder at the top of the list
            tsFiles.PropertyFileDirectories.Reverse();
            tsFiles.PropertyFileDirectories.Add(model.GetInfo().ModelPath + "\\attributes\\");
            tsFiles.PropertyFileDirectories.Reverse();

            // Ensure all directories end with "\\"
            for (int i = 0; i < tsFiles.PropertyFileDirectories.Count; i++)
            {
                if (!tsFiles.PropertyFileDirectories[i].EndsWith("\\")) tsFiles.PropertyFileDirectories[i] += "\\";
            }

            filterWatchers = new FileSystemWatcher[tsFiles.PropertyFileDirectories.Count];
            settingWatchers = new FileSystemWatcher[tsFiles.PropertyFileDirectories.Count];

            for (int i = 0; i < tsFiles.PropertyFileDirectories.Count; i++)
            {
                // Configuration for filter files
                filterWatchers[i] = new FileSystemWatcher(tsFiles.PropertyFileDirectories[i], "*." + FILTER_FILE_EXTENSION)
                {
                    IncludeSubdirectories = true,
                    EnableRaisingEvents = true
                };
                filterWatchers[i].Created += OnFilterCollectionChanged;
                filterWatchers[i].Deleted += OnFilterCollectionChanged;

                // Configuration for setting files
                settingWatchers[i] = new FileSystemWatcher(tsFiles.PropertyFileDirectories[i], "*." + SETTING_EXTENSION)
                {
                    IncludeSubdirectories = true,
                    EnableRaisingEvents = true
                };
                settingWatchers[i].Created += OnSettingCollectionChanged;
                settingWatchers[i].Deleted += OnSettingCollectionChanged;
            }
            Filters = tsFiles.GetMultiDirectoryFileList(FILTER_FILE_EXTENSION, false);
            Settings = tsFiles.GetMultiDirectoryFileList(SETTING_EXTENSION, false);
        }

        /// <summary>
        /// Updates list of available settings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSettingCollectionChanged(object sender, FileSystemEventArgs e)
        {
            Settings = tsFiles.GetMultiDirectoryFileList(SETTING_EXTENSION, false);
        }

        /// <summary>
        /// Updates list of available selection filters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFilterCollectionChanged(object sender, FileSystemEventArgs e)
        {
            Filters = tsFiles.GetMultiDirectoryFileList(FILTER_FILE_EXTENSION, false);
        }

        /// <summary>
        /// Performs the logic related to applying rules.
        /// </summary>
        /// <param name="p">Progress object.</param>
        private void PerformApplyRules(Progress p)
        {
            for (int i = 0; i < Rules.Count; i++)
            {
                var msg = $"Processing rule {i + 1}/{rules.Count}";
                Log.Info(msg);
                p.Report(new ProgressBase.Update(msg, i * 100.0 / Rules.Count, true));

                // Check if property name exist in any mapping to avoid possible unnecessary iteration
                if (!typeMap.Values.Any(x => x.Functions.Keys.Any(y => y == Rules[i].PropertyName)))
                {
                    Log.Warn($"\tProvided property name \"{Rules[i].PropertyName}\" is not supported.");
                    continue;
                }
                // Get enumerator of objects matching filter definition
                var objectsEnumerator = new Model().GetModelObjectSelector().GetObjectsByFilterName(Rules[i].FilterName);
                while (objectsEnumerator.MoveNext())
                {
                    if (objectsEnumerator.Current == null) continue;
                    // Get types inheritance hierarchy and corresponding mapping
                    var typesTree = GetInheritanceHierarchy(objectsEnumerator.Current.GetType());
                    var map = typeMap[typesTree.First(t => typeMap.Keys.Contains(t))];

                    if (map.Functions.ContainsKey(Rules[i].PropertyName))
                    {
                        if (!map.Functions[Rules[i].PropertyName].Invoke(Rules[i].PropertyValue, objectsEnumerator.Current))
                        {
                            Log.Error($"\tFailed to modify item guid: {objectsEnumerator.Current.Identifier.GUID.ToString().ToUpper()}");
                        }
                    }
                    else
                    {
                        Log.Warn($"\tProperty \"{Rules[i].PropertyName}\" not set for object guid: {objectsEnumerator.Current.Identifier.GUID.ToString().ToUpper()}, unsupported type {objectsEnumerator.Current.GetType()}");
                    }
                }
                // Handle cancel request
                if (p.IsCancellationRequested)
                {
                    Log.Info("Operation aborted by user.");
                    break;
                }
            }

            model.CommitChanges();
            if (!p.IsCancellationRequested) Log.Info("All rules applied.");
            p.Complete();
            p.Dispose();
        }

        #endregion

        #region Commands

        /// <summary>
        /// Loads a setting.  
        /// </summary>
        [CommandHandler]
        public void LoadSetting()
        {
            var filename = SelectedSetting + "." + SETTING_EXTENSION;
            var dir = tsFiles.PropertyFileDirectories.First(d => Directory.EnumerateFiles(d).Contains(d + filename));
            using (Stream fs = new FileStream(dir + filename, FileMode.Open))
            {
                Rules = (ObservableCollection<Rule>)serializer.Deserialize(fs);
            }
            Log.Info($"Loaded settings from file {Path.Combine(dir, filename)}");
        }

        /// <summary>
        /// Saves setting with given name.
        /// </summary>
        [CommandHandler]
        public void SaveSetting()
        {
            var filename = NewSetting + "." + SETTING_EXTENSION;
            var dir = model.GetInfo().ModelPath + "\\attributes\\";
            using (StreamWriter sw = new StreamWriter(dir + filename))
            {
                serializer.Serialize(sw, Rules);
            }
            SelectedSetting = NewSetting;
            Log.Info($"Saved settings to file {Path.Combine(dir, filename)}");
        }

        /// <summary>
        /// Applies the rules to model objects.
        /// </summary>
        [CommandHandler]
        public void ApplyRules()
        {
            Host.UI.ShowProgressDialog(PerformApplyRules);
        }

        /// <summary>
        /// Removes currently selected rule.
        /// </summary>
        [CommandHandler]
        public void RemoveSelectedRule()
        {
            if (SelectedRuleIndex > -1)
            {
                Rules.RemoveAt(SelectedRuleIndex);
            }
        }

        /// <summary>
        /// Adds rule to the set.
        /// </summary>
        [CommandHandler]
        public void AddRule()
        {
            Rules.Add(new Rule()
            {
                FilterName = Filters[SelectedFilterIndex],
                PropertyName = AttributeName,
                PropertyValue = AttributeValue
            });
        }

        #endregion
    }
}
