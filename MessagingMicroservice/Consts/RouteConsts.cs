using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingMicroservice.Localization {
    public class RouteConsts {

        public const string ROUTE_API_BASE = "/api";

        public const string ROUTE_MESSAGING_BASE = ROUTE_API_BASE + "/messaging";//TODO CREATE, UPDATE, DELETE
        public const string ROUTE_MESSAGE_GET_ONE_BY_UUID = ROUTE_MESSAGING_BASE + "/{uuid}";//TODO Podaci o jednoj poruci
        public const string ROUTE_GET_CONVERSATIONS_BY_USER = ROUTE_MESSAGING_BASE + "/conversations";//TODO Treba da vrati sve recipente sa kojima je korisnik razmenio bar jednu poruku
        public const string ROUTE_MESSAGE_GET_BY_RECIPEINT = ROUTE_MESSAGING_BASE + "/{recipientUUID";//TODO Vraca sve razmenjene poruke sa jednim korisnikom
        public const string ROUTE_FILES_BY_MESSAGE_UUID = ROUTE_MESSAGING_BASE + "/{messageUUID}";//TODO Skidanje fajla za jednu poruku
    }
}
