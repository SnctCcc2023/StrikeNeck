using Microsoft.Data.Sqlite;
using System;
using System.Windows;

namespace UseDataControl.DataControl
{
    class SQLiteCommandExecutor
    {
        public void RunNonQueryCommand(string query)
        {
            try
            {
                // 接続先を指定
                using (var conn = new SqliteConnection("Data Source=DataBase.sqlite")) //エラーが出ないか後ほど確認するように
                using (var command = conn.CreateCommand())
                {
                    // 接続
                    conn.Open();

                    // コマンドの実行処理
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    //var value = command.ExecuteNonQuery();
                    //MessageBox.Show($"更新されたレコード数は {value} です。");
                }
            }
            catch (Exception ex)
            {
                //例外が発生した時はメッセージボックスを表示
                MessageBox.Show(ex.Message);
            }
        }
    }
}
