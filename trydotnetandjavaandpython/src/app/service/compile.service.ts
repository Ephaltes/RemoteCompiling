import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CodeModel } from '@ngstack/code-editor';
import { FileCode } from '../file-code';
export interface CompileCode {
  data: {
    compile?: { code: number, signal: any, stderr: "", stdout: "" },
    run: { code: number, signal: any, stderr: "", stdout: "" }
  };
}

@Injectable()
export class CompileService {

  constructor(private http: HttpClient) { }
  public compile(version: string, code: CodeModel, runFiles: FileCode[]) {
    return this.http.post<CompileCode>(`/Api/Compile`, { language: code.language, version: version, code: { args: [""], stdin: "", mainFile: code.uri, files: runFiles } });
  }
}
