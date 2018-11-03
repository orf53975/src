import datetime as dt

class IBAccount(object):
    """description of class"""
    UserName = None
        properties = {}
        for key in self.__dict__:
            properties[key] = self.__dict__[key]
        return str(properties)

    def set_value(self, field, value):
        def to_int(val):
            ival = None
            try:
                ival = int(val)
            except:
                pass
            return ival

        def to_float(val):
            fval = None
            try:
                fval = float(val)
            except:
                pass
            return fval

        def to_str(val):
            return str(val)
        
        def to_datetime(val):
            try:
                return dt.datetime.strptime(val, "%Y-%m-%d %H:%M:%S")
            except:
                pass
            return None

        set_functions = {
            'UserName' : to_str,

        if field in set_functions:
            self.__dict__[field] = set_functions[field](value)