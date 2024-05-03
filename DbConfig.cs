namespace FastSql
{
    public class DbConfig
    {
        /// <summary>
        /// 数据库配制
        /// </summary>
        internal static string SqlConnectString { get; private set; }

        /// <summary>
        /// 设置数据库连接
        /// </summary>
        /// <param name="constr"></param>
        public static void SetConnectString(string constr) {

            SqlConnectString = constr;

        }
    }
}
