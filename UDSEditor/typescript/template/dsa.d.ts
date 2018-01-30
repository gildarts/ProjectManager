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
 * UserName、PublicUrl、ApplicationUrl、ApplicationName、sessionid
 * @param name 屬性名稱：UserName、PublicUrl、ApplicationUrl、ApplicationName、sessionid。
 */
declare function getContextProperty(name: string): string;

/**
 * DSA: 執行 SQL。
 * @param cmd 要執行的 SQL。
 */
declare function executeSql(cmd: string): ResultSet;

/**
 * DSA: 輸出訊息到 DSA Header Console 區段。
 * @param msg 訊息。
 */
declare function print(msg: string): void;

/**
 * DSA: 丟出 DSA 504 錯誤，並自定錯誤代碼。
 * @param msg 錯誤訊息。
 * @param code 代碼。
 */
declare function throwError(msg: string, code: string | number): void

/**
 * DSA: 設定 DSA Response Header。
 * @param name 名稱。
 * @param headerObj 輸出物件，必須是 Json 物件。
 */
declare function setResponseHeader(name: string, headerObj: any): void;

/**
 * DSA: 取得 DSA Resource 字串。
 * @param name Resource 名稱。
 */
declare function getResource(name: string): string;

/**
 * DSA: 不經處理直接輸出資料。
 * @param content 輸出內容。
 */
declare function raw(content: any): any;

/**
 * DSA: 取得資料庫時間，例：new Date(getDBDateTime()).format('yyyy-MM-dd');
 */
declare function getDBDateTime(): any;

/**
 * DSA: 建立另一資料庫連線，DSA 需要定義才可使用。
 * @param name 名稱。
 */
declare function newConnection(name: string): any;

/**
 * DSA: 你懂的，少用。
 */
declare function commitTransaction(): any;

/**
 * DSA: 你懂的，少用。
 */
declare function rollbackTransaction(): any;

/**
 * DSA: 工具類別。
 */
declare class util {
    /**
     * DSA: 使用 Java 進行日期格式轉換。
     * @param dateString 日期字串。
     * @param pattern 輸出格式。
     */
    toDate(dateString: string, pattern: string): any;
}

/**
 * DSA: SQL 執行結果。
 */
declare class ResultSet {
    /**
     * 將資料直接轉成陣列。
     */
    toArray(): any[];
    /**
     * 讀取下一筆。
     */
    next(): boolean;
    /**
     * 取得指定欄位資料。
     * @param name 欄位名稱。
     */
    get(name: string): string;
    /**
     * 取得欄位清單。
     */
    getColumns(): string[];
}

/**
 * DSA: HTTP Get。
 * @param url URL。
 */
declare function httpGet(url: string): any;

/**
 * DSA: HTTP Post。
 * @param url URL。
 * @param data Data。
 */
declare function httpPost(url: string, data: any): any;

/**
 * DSA: Request 參數。
 */
declare interface RequestArguments {
    /**
     * URL。
     */
    url: string;
    /**
     * HTTP Method，支援全部。
     */
    method: string;
    /**
     * HTTP Header。
     */
    header: any;
    /**
     * HTTP Body Data。
     */
    body?: any;
    /**
     * Timeout，以秒為單元，預設 60 秒。
     */
    timeout?: number;
}

/**
 * DSA: 使用 JSON 呼叫 HTTP Service。
 * @param args HTTP Request 參數。
 */
declare function simpleHttpRequest(args: RequestArguments): any;

/**
 * DSA: 計算資料雜湊值。
 * @param value 要計算的值。
 */
declare function computeSHA1Hex(value: string): string;

/**
 * DSA: 將 XML 轉碼成可以傳送格式。
 * @param data XML 資料。
 */
declare function encodeXml(data: string): any;

/**
 * DSA: 將字串解碼為 XML 資料。
 * @param data 需解碼的 XML 資料。
 */
declare function decodeXml(data: string): any;

/**
 * DSA: 資料庫工具。
 */
declare interface dbutil {
    /**
     * DSA: 將 JSResultSet 轉換成 JS Array。
     */
    toArray(resultSet: ResultSet): any;
}

declare interface Date {
    /**
     * DSA: 格式化日期格式。
     */
    format(pattern: string): string;
}
