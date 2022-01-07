import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import * as globalVar from '../../../globals'
export interface ScanBody {
    id: number;
    status: ScanStatus;
}
export interface Scan {
    total: number;
    issues: ScanIssues[];
}
export interface ScanIssues {
    type: IssueType;
    severity: Severity;
    component: string;
    message: string;
    textLocation: TextLocation;
}
enum ScanStatus {
    Pending = 0,
    Available = 1,
    Failed = 2
}
enum Severity {
    Major = 0,
    Minor = 1
}
enum IssueType {
    CodeSmell = 0,
    Bug = 1,
    Vulnerability = 2
}
export interface TextLocation {
    startLine: number;
    endLine: number;
    startOffset: number;
    endOffset: number;
}

@Injectable()
export class StaticCodeService {
    constructor(private http: HttpClient) { }
    public postScan(code: string, language: string) {
        return this.http.post<ScanBody>(globalVar.apiURL + "/scans", { code: code, codeLanguage: language })
    }
    public getScanResult(scanId: number) {
        return this.http.get<Scan>(globalVar.apiURL + "/scans/" + scanId + "/results")
    }
}