using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PawnStoreSetupTool.Data
{
    public static class StoreTypeAndStoreProductQueries
    {

        public const string STORETYPE_QUERIES = "INSERT INTO CCSOWNER.STORETYPE (ID," +
            " STORENUMBER," +
            " CREATEDBY," +
            " UPDATEDBY," +
            " CREATIONDATE," +
            " LASTUPDATEDATE," +
            " IS_TOPS_SAFE," +
            " IS_INTEGRATED," +
            " IS_PAWN_PRIMARY," +
            " IS_TOPS_EXIST)" +
            "  VALUES   ( (  SELECT   MAX (ID) + 1 FROM ccsowner.storetype)," +
            "  '?STORE_NO?'," +
            "  'SEEDDATA'," +
            "  'SEEDDATA'," +
            "  SYSDATE," +
            "  SYSDATE," +
            "  '0'," +
            "  '1'," +
            "  '1'," +
            "  '0');";

        public const string STOREPRODUCT_QUERIES1 = "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     '00033'," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'PawnButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'DescribeMerchandiseButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'NewPawnLoanButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'ServicePawnLoansButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'RetailButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'BuyButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     '00034'," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'CustomerBuyButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'BuyLookupButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );";

        public const string STOREPRODUCT_QUERIES2 = "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'BuyDescribeButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     '00034'," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'VendorBuyButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'LookupButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'LookupCustomerButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'LookupDocumentsButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'LookupTicketButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'ManualButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'NFSCButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );";

        public const string STOREPRODUCT_QUERIES3 = "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'UtilityButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'AssignItemPhysicalLocationButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'PFIButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'PFICreateListButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'PFIVerifyButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'PFIPostButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'TransferButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'Police_ActivitiesButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );";

        public const string STOREPRODUCT_QUERIES4 = "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'PoliceHoldButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'PoliceSeizeButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'ItemReleaseButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'CustomerHoldsButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'CustomerHoldsAddHoldButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'CustomerHoldsEditReleaseHoldButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'Update_security_profileButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'Gun_Book_UtilitiesButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'VoidTransactionButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );";

        public const string STOREPRODUCT_QUERIES5 = "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'VoidLoanButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'VoidBuyButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'VoidBuyReturnsButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'VoidCashTransferButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'VoidShopToShopTransferButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'Manage_cashButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'BalanceButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'ShopCashManagementButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );";

        public const string STOREPRODUCT_QUERIES6 = "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'refundbutton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'ReturnCustomerBuyButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'ReturnVendorBuyButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'InternalTransferButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'BankTransferOutButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'BankTransferInButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'ShopTransferOutButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );";

        public const string STOREPRODUCT_QUERIES7 = "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'ShopTransferInButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'ReportsButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'Loan_AuditButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'Gun_AuditButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'In_Pawn_jewelry_LocationsButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'Full_LocationsButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'TransferCashButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'MerchandiseTransferOutButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );";

        public const string STOREPRODUCT_QUERIES8 = "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'CashTransfer')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'SaleRefundButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'RefundSaleButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'VoidSaleButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'VoidSaleRefundButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'VoidLayawayButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'BalanceCashDrawerButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'BalanceSafeButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );";

        public const string STOREPRODUCT_QUERIES9 = "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'RefundLayawayButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'ManageInventoryButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'ChangePricingButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'LayawayForfeitureButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'MerchandiseTransferINButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'ChangeRetailPriceButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'ChangeItemAssignTypeButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'VoidBankTransferButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'ChargeOffButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );";

        public const string STOREPRODUCT_QUERIES10 = "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'balanceotherdrawersButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'cashdrawerButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'cashdrawermanagementButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'cashdrawertransferButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'managecashButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'safeoperationsButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'shoptransferinButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'shoptransferoutButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'gunbookeditButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );";

        public const string STOREPRODUCT_QUERIES11 = "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'gunbookprintButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'masspricereductionButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );" +

            "INSERT INTO CCSOWNER.STOREPRODUCTS (" +
            "  STORE_PRODUCT_ID," +
            "  STORE_TYPE_ID," +
            "  PRODUCT_ID," +
            "  STORE_PRODUCT_MENU_ID," +
            "  CREATIONDATE," +
            "  CREATEDBY" +
            " )" +
            "  VALUES   (" +
            "     (SELECT   MAX (STORE_PRODUCT_ID) + 1" +
            "  FROM   CCSOWNER.STOREPRODUCTS)," +
            "     (SELECT   ID" +
            "  FROM   CCSOWNER.STORETYPE ST" +
            " WHERE   ST.STORENUMBER = '?STORE_NO?')," +
            "     NULL," +
            "     (SELECT   SPM.ID" +
            "  FROM   CCSOWNER.STOREPRODUCTMENU SPM" +
            " WHERE   SPM.TAG_NAME = 'merchandisecostrevisionsButton')," +
            "     SYSDATE," +
            "     'SEEDDATA'" +
            " );";
    }
}
