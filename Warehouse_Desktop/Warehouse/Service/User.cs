using System;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using SqlServerDAL; // 项目 SqlServer 的 DbHelperSQL 类

using System.Data.OleDb;

namespace Warehouse
{
    public partial class User
    {
        public User() { }

        #region Model
        private int _userid;
        private string _username;
        private string _userpwd;
        private string _position;

        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }

        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }

        public string UserPwd
        {
            set { _userpwd = value; }
            get { return _userpwd; }
        }

        public string Position
        {
            set { _position = value; }
            get { return _position; }
        }
        #endregion Model

        #region  Method
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public User(int UserID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select UserID,UserName,UserPwd,[Position] ");    // position 为 Access 保留字，加 []
			strSql.Append(" FROM [User] ");
			strSql.Append(" where UserName=@UserName and UserID=@UserID ");
			OleDbParameter[] parameters = {
					new OleDbParameter("@UserName", UserName),
					new OleDbParameter("@UserID", UserID)};

			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["UserID"]!=null && ds.Tables[0].Rows[0]["UserID"].ToString()!="")
				{
					this.UserID=int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UserName"]!=null)
				{
					this.UserName=ds.Tables[0].Rows[0]["UserName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["UserPwd"]!=null)
				{
					this.UserPwd=ds.Tables[0].Rows[0]["UserPwd"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Position"]!=null)
				{
					this.Position=ds.Tables[0].Rows[0]["Position"].ToString();
				}
			}
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string UserName)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from [User]");
			strSql.Append(" where UserName=@UserName");

			OleDbParameter[] parameters = {
					new OleDbParameter("@UserName", UserName)};

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Login(string UserName,string userPwd)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from [User]");
            strSql.Append(" where UserName=@UserName AND UserPwd = @userPwd");

            OleDbParameter[] parameters = {
					new OleDbParameter("@UserName", UserName),
                    new OleDbParameter("@userPwd", userPwd)};

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into [User] (");
			strSql.Append("UserName,UserPwd,[Position])");  // position 是 Access 的保留字，所以需要加 []
			strSql.Append(" values (");
			strSql.Append("@UserName,@UserPwd,@Position);");
			//strSql.Append(";select @@IDENTITY");  // SQL Server 用
			OleDbParameter[] parameters = {
					new OleDbParameter("@UserName", OleDbType.VarWChar,50),
					new OleDbParameter("@UserPwd", OleDbType.VarWChar,50),
					new OleDbParameter("@Position", OleDbType.VarWChar,50)};
			parameters[0].Value = UserName;
			parameters[1].Value = "123456";
			parameters[2].Value = Position;

			//object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters); // SQL Server 用
            object obj = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update [User] set ");
			strSql.Append("UserPwd=@UserPwd,");
			strSql.Append("[Position]=@Position");  // position 为 Access 保留字，需要加 []
			strSql.Append(" where UserName=@UserName");
			OleDbParameter[] parameters = {
					new OleDbParameter("@UserPwd", OleDbType.VarChar,50),
					new OleDbParameter("@Position", OleDbType.VarChar,50),
					new OleDbParameter("@UserName", OleDbType.VarChar,50)};
			parameters[0].Value = UserPwd;
			parameters[1].Value = Position;
			parameters[2].Value = UserName;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete()
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from [User] ");
			strSql.Append(" where UserName=@UserName");
			OleDbParameter[] parameters = {
					new OleDbParameter("@UserName", UserName)};

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public void GetModel(string name)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select UserID,UserName,UserPwd,[Position] ");    // position 为 Access 关键字，加 []
			strSql.Append(" FROM [User] ");
			strSql.Append(" where UserName=@UserName");
			OleDbParameter[] parameters = {
					new OleDbParameter("@UserName", name)};

			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["UserID"]!=null && ds.Tables[0].Rows[0]["UserID"].ToString()!="")
				{
                    this.UserID = int.Parse(ds.Tables[0].Rows[0]["UserID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["UserName"]!=null )
				{
                    this.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["UserPwd"]!=null )
				{
                    this.UserPwd = ds.Tables[0].Rows[0]["UserPwd"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Position"]!=null )
				{
                    this.Position = ds.Tables[0].Rows[0]["Position"].ToString();
				}
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select * ");
			strSql.Append(" FROM [User] ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		#endregion  Method
	}
}
