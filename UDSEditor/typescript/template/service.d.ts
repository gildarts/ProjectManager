// 放入您自已的型別定義檔案。
// 使用方式可參考 dsa.d.ts。

declare let Base64: any;

declare class PKCS8EncodedKeySpec {
    constructor(keyBytes: any);
}

declare class KeyFactory {
    /**
     * 取得實體。
     * @param name 演算法名稱：DSA、RSA。
     */
    static getInstance(name: string): any;
}

declare let GoogleNetHttpTransport: any;

declare let GoogleCredential: any;

declare let Storage: any;