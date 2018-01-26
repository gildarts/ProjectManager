// DSA 核心提供的相關功能定議。

/**
 * DSA: 匯入 Java Library。
 * @param packageId 套件識別。
 */
declare function importPackage(packageId: any): void;

/**
 * DSA: 取得用戶端傳送過來的 Request。
 */
declare function getRequest(): any;

/**
 * DSA: 目前登入者的屬性。
 * @param name 屬性名稱：Account、StudentID、TeacherID、IsParent、sessionid 不一定每個都有。
 */
declare function getContextProperty(name: string): string;

/**
 * DSA: 執行 SQL。
 * @param cmd 要執行的 SQL。
 */
declare function executeSql(cmd: string): ResultSet;

/**
 * DSA: SQL 執行結果。
 */
declare class ResultSet {
    toArray(): any;
}

/**
 * DSA: java 相關程式庫。
 */
declare let java: any;

declare let Packages: any;

declare let com: any;

declare let JacksonFactory: any;

/**
 * DSA: Java 集合類別。
 */
declare let ArrayList: any;