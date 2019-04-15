using Dropbox.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DropboxUploader
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var task = Task.Run(Run);
            task.Wait();

        }

        async Task Run()
        {
            using (var client = new DropboxClient("L7TvKjZYxoAAAAAAAAAADn7G1h7Lls_htjoYETDKS_frWVe-EhcV_vvFN_prKYS7"))
            {
                //var full = await dbx.Users.GetCurrentAccountAsync();
                //Console.WriteLine("{0} - {1}", full.Name.DisplayName, full.Email);
                //await CreateFolder(client);
                await ListRootFolder(client);
            }
        }

        async Task ListRootFolder(DropboxClient client)
        {
            //var list = await client.Files.ListFolderAsync(string.Empty);
            //var list = await client.Files.ListFolderAsync("/samplefolder");
            //var list2 = await client.Files.ListFolderAsync("/testfolder");

            // show folders then files
            await CheckFolder(client, string.Empty);

            //foreach (var item in list.Entries.Where(i => i.IsFolder))
            //{
            //    Console.WriteLine("D  {0}/", item.Name);
            //    Dropbox.Api.Files.Metadata file = item;
                
            //}

            //foreach (var item in list.Entries.Where(i => i.IsFile))
            //{
            //    Console.WriteLine("F{0,8} {1}", item.AsFile.Size, item.Name);
            //}
        }

        async Task CheckFolder(DropboxClient client, string path, Dropbox.Api.Files.ListFolderResult list=null)
        {
            var list2 = await client.Files.ListFolderAsync(path);
            foreach (var item in list2.Entries.Where(i => i.IsFolder))
            {
                if (item.IsFolder)
                {
                    Console.WriteLine("Path: {0}", item.PathDisplay);
                    await CheckFolder(client, item.PathDisplay);
                }
            }
        }

        async Task CreateFolder(DropboxClient client)
        {
            await client.Files.CreateFolderV2Async("/samplefolder/folder2");
        }


    }
}
