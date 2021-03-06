﻿// Ai Software Library.

using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

namespace Ai.Common {
    public sealed class Currency : DataObject {
        int _id = 0;
        string _name = "";
        string _sign = "";
        public Currency(Connection conn) : base(conn) {
            setTmp("ID", _id);
            setTmp("Name", _name);
            setTmp("Sign", _sign);
        }
        public int ID { get { return _id; } }
        public string Name {
            get { return _name; }
            set  {
                if (_name != value) {
                    setTmp("Name", _name);
                    _name = value;
                }
            }
        }
        public string Sign {
            get { return _sign; }
            set {
                if (_sign != value) {
                    setTmp("Sign", _sign);
                    _sign = value;
                }
            }
        }
        public static bool keyIsExist(Connection conn, object[] keys) {
            if (keys == null) return false;
            if (keys.Length < 1) return false;
            string strSQL;
            if (keys.Length == 1) strSQL = "SELECT COUNT(1) FROM TblCurrency WHERE currencyName='" +
                Connection.formatValue((string)keys[0]) + "'";
            else if (keys.Length == 2) strSQL = "SELECT COUNT(1) FROM TblCurrency WHERE currencyName='" +
                Connection.formatValue((string)keys[0]) + "' AND currencyID<>" + (int)keys[1];
            else return false;
            return executeCommand(strSQL, conn);
        }
        public override bool undoValue(string propName) {
            switch (propName) { 
                case "Name":
                    _name = (string)getTmp(propName);
                    return true;
                case "Sign":
                    _name = (string)getTmp(propName);
                    return true;
                default:
                    return false;
            }
        }
        public override bool save() {
            if (Connection.formatValue(_name) == "") return false;
            if (keyIsExist(_connection, new object[] { _name })) return false;
            string strSQL = "SELECT sqcCurrencyID.nextVal FROM dual";
            if (_connection.executeData(strSQL, Common.getCaller())) {
                DbDataReader dr = _connection.Reader;
                if (dr.HasRows) {
                    dr.Read();
                    _id = dr.GetInt32(0);
                    dr.Close();
                    dr.Dispose();
                    strSQL = "INSERT INTO TblCurrency VALUES(" + _id + ", '" +
                        Connection.formatValue(_name) + "', '" + Connection.formatValue(_sign) + "')";
                    return _connection.executeSQL(strSQL, Common.getCaller());
                }
                dr.Close();
                dr.Dispose();
            }
            return false;
        }
        public override bool load(object key) {
            if (key == null) return false;
            if ((int)key < 1) return false;
            string strSQL = "SELECT * FROM TblCurrency WHERE currencyID=" + _id;
            if (_connection.executeData(strSQL, Common.getCaller())) {
                DbDataReader dr = _connection.Reader;
                if (dr.HasRows) {
                    dr.Read();
                    _id = dr.GetInt32(0);
                    _name = dr.GetString(1);
                    _sign = dr.GetString(2);
                    dr.Close();
                    dr.Dispose();
                    return true;
                }
                dr.Close();
                dr.Dispose();
            }
            return false;
        }
        public override bool modify() {
            if (_id < 1) return false;
            if (Connection.formatValue(_name) == "") return false;
            if (keyIsExist(_connection, new object[] { _name, _id })) return false;
            string strSQL = "UPDATE TblCurrency SET (currencyName, currencySign)=('" +
                Connection.formatValue(_name) + "', '" + Connection.formatValue(_sign) +
                "') WHERE currencyID=" + _id;
            return _connection.executeSQL(strSQL, Common.getCaller());
        }
        public override bool remove() {
            if (_id < 1) return false;
            string strSQL = "DELETE FROM TblCurrency WHERE currencyID=" + _id;
            return _connection.executeSQL(strSQL, Common.getCaller());
        }
    }
}