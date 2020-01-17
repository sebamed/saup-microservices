using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Consts {
    public class RoleConsts {

        public const string ROLE_ADMIN = "Administrator";
        public const string ROLE_STUDENT = "Student, Administrator";
        public const string ROLE_TEACHER = "Nastavnik, Administrator";
        public const string ROLE_STUDENT_ONLY = "Student";
        public const string ROLE_TEACHER_ONLY = "Nastavnik";

        public const string ROLE_USER = "Administrator, Student, Nastavnik";

    }
}
