﻿// Ai Software Library.

using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

namespace Ai.Common {
    public sealed class Menu : DataObject {
        int _id = 0;
        string _name = "";
        ObjectLanguageManager _olm;
        public Menu(Connection conn) : base(conn) {
            setTmp("ID", _id);
            setTmp("Name", _name);
            _olm = new ObjectLanguageManager(_connection);
            _olm.addField("menuText", "Text");
            _olm.addField("menuToolTip", "ToolTip");
            _olm.addField("menuToolTipText", "ToolTipText");
            _olm.TableName = "TblMenuDetail";
            _olm.KeyName = "menuID";
        }
        public int ID { get { return _id; } }
        public string Name {
            get { return _name; }
            set {
                if (_name != value) {
                    setTmp("Name", _name);
                    _name = value;
                }
            }
        }
        public ObjectLanguageManager LanguageManager { get { return _olm; } }
        public override bool undoValue(string propName) {
            switch (propName) { 
                case "Name":
                    _name = (string)getTmp(propName);
                    return true;
                default:
                    return false;
            }
        }
        public static bool keyIsExist(Connection conn, object[] keys) {
            if (keys == null) return false;
            if (keys.Length < 1) return false;
            string strSQL;
            if (keys.Length == 1) strSQL = "SELECT COUNT(1) FROM TblMenu WHERE menuName='" +
                Connection.formatValue((string)keys[0]) + "'";
            else if (keys.Length == 2) strSQL = "SELECT COUNT(1) FROM TblMenu WHERE menuName='" +
                Connection.formatValue((string)keys[0]) + "' AND menuID<>" + (int)keys[1];
            else return false;
            return executeCommand(strSQL, conn);
        }
        public override bool save() {
            if (Connection.formatValue(_name) == "") return false;
            if (keyIsExist(_connection, new object[] { _name })) return false;
            string strSQL = "SELECT sqcMenuID.nextVal FROM dual";
            if (_connection.executeData(strSQL, Common.getCaller())) {
                DbDataReader dr = _connection.Reader;
                if (dr.HasRows) {
                    dr.Read();
                    _id = dr.GetInt32(0);
                    dr.Close();
                    dr.Dispose();
                    strSQL = "INSERT INTO TblMenu VALUES(" + _id + ", '" 
                        + Connection.formatValue(_name) + "')";
                    if (_connection.executeSQL(strSQL, Common.getCaller())) {
                        _olm.ObjectID = _id;
                        return true;
                    } else {
                        return false;
                    }
                }
                dr.Close();
                dr.Dispose();
            }
            return false;
        }
        public override bool load(object key) {
            if (key == null) return false;
            if ((int)key < 1) return false;
            string strSQL = "SELECT * FROM TblMenu WHERE menuID=" + (int)key;
            if (_connection.executeData(strSQL, Common.getCaller())) {
                DbDataReader dr = _connection.Reader;
                if (dr.HasRows) {
                    dr.Read();
                    _id = dr.GetInt32(0);
                    _name = dr.GetString(1);
                    dr.Close();
                    dr.Dispose();
                    _olm.ObjectID = _id;
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
            string strSQL = "UPDATE TblMenu SET menuName='" + Connection.formatValue(_name) + "' WHERE menuID=" + _id;
            return _connection.executeSQL(strSQL, Common.getCaller());
        }
        public override bool remove() {
            if (_id < 1) return false;
            string strSQL = "DELETE FROM TblMenu WHERE menuID=" + _id;
            return _connection.executeSQL(strSQL, Common.getCaller());
        }
    }
}