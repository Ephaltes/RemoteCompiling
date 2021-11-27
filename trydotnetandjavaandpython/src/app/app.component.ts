import { Component, OnDestroy, OnInit } from "@angular/core";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})


export class AppComponent implements OnInit, OnDestroy {
  ngOnDestroy(): void {
    console.log("app destroy");
  }
  ngOnInit(): void {
    console.log("app init");
  }
<<<<<<< HEAD
  removeNode(node: FileNode) {
    if (this.isNodeSelected(node) || ) {
      this.selectedModel = { uri: "", language: "", value: "" };
    }
    this.database.remove(node);
    this.refreshTree();
  }
  onEditorLoaded() {
    console.log('Online Editor loaded!');
  }
  runCode() {
    this.runFiles = [];
    if (this.selectedModel.language == 'csharp') {
      this.selectedVersion = this.selectedCSharpVersion;
      this.nestedDataSource.data.filter(item => item.type == FileNodeType.csharp ? this.runFiles.push(new FileCode(item.code.uri, item.code.value)) : false);
    }
    if (this.selectedModel.language == 'java') {
      this.selectedVersion = this.selectedJavaVersion;
      this.nestedDataSource.data.filter(item => item.type == FileNodeType.java ? this.runFiles.push(new FileCode(item.code.uri, item.code.value)) : false);
    }
    if (this.selectedModel.language == 'python') {
      this.selectedVersion = this.selectedPythonVersion;
      this.runFiles.push(new FileCode(this.selectedModel.uri, this.selectedModel.value));
    }

    if (this.selectedModel.language == 'cpp') {
      this.selectedVersion = this.selectedCppVersion;
      this.nestedDataSource.data.filter(item => item.type == FileNodeType.cpp ? this.runFiles.push(new FileCode(item.code.uri, item.code.value)) : false);
    }

    if (this.selectedModel.language == 'c') {
      this.selectedVersion = this.selectedCVersion;
      this.nestedDataSource.data.filter(item => item.type == FileNodeType.c ? this.runFiles.push(new FileCode(item.code.uri, item.code.value)) : false);
    }

    this.isLoading = true;
    this.output = "Loading...";
    this.compileService.compile(this.selectedVersion, this.selectedModel, this.runFiles).subscribe((item => { this.isLoading = false; item.data.run.stderr.length > 0 ? this.output = item.data.run.stderr : this.output = item.data.run.stdout }))

  }
  openNewFolderDialog() {
    const dialogRef = this.Dialog.open(AddNewFolderComponent, { data: this.database.fileNames() });
    dialogRef.afterClosed().subscribe(result => { dialogRef.componentInstance.validData ? this.addNewFolder(dialogRef.componentInstance.emittingData) : false })
  }
  openNewFileDialog(node:FileNode) {
    const dialogRef = this.Dialog.open(AddNewFileComponent, { data: this.database.fileNamesForFolders(node.name) });
    dialogRef.afterClosed().subscribe(result => { dialogRef.componentInstance.validData ? this.addNewFile(node.name,dialogRef.componentInstance.emittingData) : false })
  }
  addNewFolder(data:any){
    this.database.addFolder(data.name);
    this.refreshTree();
  }
  addNewFile(folderName:string,data: any) {
    this.database.addFile(folderName,data.name, data.language, data.code);
    this.refreshTree();
  }
  
  ngOnInit() {
    var toast: Toast = {
      type: 'success',
      title: 'Auto save complete',
      showCloseButton: false
    };
    this.saveSource.subscribe(val => { this.database.save(), this.toasterSerivce.pop(toast) });
    this.nestedTreeControl.expand(this.nestedDataSource.data[0]);
    this.selectNode(this.nestedDataSource.data[0].children[0]);
  }
  ngOnDestroy() {
    this.database.save();
    this.subscription.unsubscribe();
  }

}
=======
}
>>>>>>> 2da48ea9ef5ffd871044c529098580b07c2a9b32
