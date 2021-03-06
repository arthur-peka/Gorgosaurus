﻿using Dapper;
using Gorgosaurus.BO.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gorgosaurus.DA.Managers
{
    public class GlobalSettingsManager
    {
        public static readonly GlobalSettingsManager Instance = new GlobalSettingsManager();

        public Dictionary<string, string> GetDefaultSettings()
        {
            var res = new Dictionary<string, string>()
            {
                { Enum.GetName(typeof(GlobalSettingsEnum), GlobalSettingsEnum.ForumName), "First forum" },
                { Enum.GetName(typeof(GlobalSettingsEnum), GlobalSettingsEnum.PageSize), "2" }
            };

            return res;
        }

        public void Save(GlobalSettingsEnum setting, string value)
        {
            using (var conn = DbConnector.GetOpenConnection())
            {
                SaveInner(setting, value, conn);
            }
        }

        private void SaveInner(GlobalSettingsEnum settingName, string value, IDbConnection conn)
        {
            conn.Execute(String.Format("Update {0} set Value = :value where Name = :settingName", typeof(GlobalSetting).Name), new { value = value, settingName = Enum.GetName(typeof(GlobalSettingsEnum), settingName) });
        }

        public string Load(GlobalSettingsEnum setting)
        {
            using (var conn = DbConnector.GetOpenConnection())
            {
                var res = conn.ExecuteScalar<string>(String.Format("select Value from {0} where Name = :name", typeof(GlobalSetting).Name),
                    new { name = Enum.GetName(typeof(GlobalSettingsEnum), setting) });

                return res;
            }
        }

        public List<GlobalSetting> LoadAll()
        {
            using (var conn = DbConnector.GetOpenConnection())
            {
                var res = conn.Query<GlobalSetting>(String.Format("select Name, Value from {0}", typeof(GlobalSetting).Name));

                return res.ToList();
            }
        }

        public void SaveAll(IEnumerable<GlobalSetting> settings)
        {
            using (var conn = DbConnector.GetOpenConnection())
            {
                foreach (var setting in settings)
                {
                    GlobalSettingsEnum typedName;
                    bool isParsingSuccessful = Enum.TryParse<GlobalSettingsEnum>(setting.Name, out typedName);

                    if (!isParsingSuccessful)
                        throw new ArgumentException("Wrong setting name");

                    SaveInner(typedName, setting.Value, conn);
                }
            }
        }
    }
}
