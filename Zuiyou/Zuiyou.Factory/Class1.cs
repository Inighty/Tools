using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zuiyou.Factory
{
    public class ConnectionFactory
    {
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="databaseName">数据库名称</param>
        /// <param name="isRead">是否为读连接</param>
        /// <returns>数据库连接</returns>
        public static IDbConnection GetConnection(string databaseName, bool isRead)
        {
            DBRouteHelper hepler = new DBRouteHelper();
            MSingleDatabaseInfo model = new MSingleDatabaseInfo();
            model.ApplicationName = "资源管理数据服务";
            model.DatabaseName = databaseName;
            model.IsRead = isRead;
            //model.BaseDBNameSwitch = true;
            model.BizTime = DateTime.Now;
            return hepler.GetSingleConnection(model);
        }
    }
}
