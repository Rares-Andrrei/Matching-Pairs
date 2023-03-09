using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace PairsGame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string FileToUsersData = "Data\\UsersData.bin";
        private UsersManager _usersManager;
        App()
        {
            _usersManager = null;
            try
            {
                _usersManager = (UsersManager)DataSerializaion.BinaryDeserialization(FileToUsersData);
            }
            catch (SerializationException)
            {
                _usersManager = new UsersManager();
            }
            if ( _usersManager == null )
            {
                _usersManager = new UsersManager();
            }
        }

    }
}
