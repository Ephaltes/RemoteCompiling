import { FileNode, FileNodeType } from "../file-module/file-node";
import { User, UserProject } from "./userproject.service";

export function convertFileTypeToNumber(fileType: FileNodeType): number {
    switch (fileType) {
        case FileNodeType.csharp:
            return 0;
        case FileNodeType.c:
            return 1;
        case FileNodeType.cpp:
            return 2;
        case FileNodeType.java:
            return 3;
        case FileNodeType.python:
            return 4;
        default:
            return 0;
    }
}
export function convertNumberToFileType(projectType: number): FileNodeType {
    var fileType: FileNodeType;
    switch (projectType) {
        case 0:
            fileType = FileNodeType.csharp;
            break;
        case 1:
            fileType = FileNodeType.c;
            break;
        case 2:
            fileType = FileNodeType.cpp;
            break;
        case 3:
            fileType = FileNodeType.java;
            break;
        case 4:
            fileType = FileNodeType.python;
            break;
        default:
            fileType = FileNodeType.csharp;
            break;
    }
    return fileType;
}
export function convertBEtoFEEntity(user: User): FileNode[] {
    var projectsConverted: FileNode[] = [];
    const projects = user.data.projects;
    if (projects != undefined) {
        projects.forEach(pj => {
            var newFileNode = new FileNode(pj.projectName, FileNodeType.folder, "")
            newFileNode.children = [];
            newFileNode.projectid = pj.id;
            newFileNode.projectType = pj.projectType;
            var fileType: FileNodeType = convertNumberToFileType(pj.projectType);
            if (pj.files.length > 0) {
                pj.files.forEach(pjFile => {
                    var checkpoint = pjFile.checkpoints.reduce((r, o) => r.created > o.created ? r : o);
                    var childFile = new FileNode(pjFile.fileName, fileType, checkpoint.code);
                    childFile.exerciseId = pj.exerciseID;
                    childFile.fileId = pjFile.id;
                    childFile.projectid = pj.id;
                    newFileNode.children.push(childFile);
                });
            }
            projectsConverted.push(newFileNode);
        });
    }
    return projectsConverted;
}
export function convertBEtoFEEntityFromUserProject(template: UserProject): FileNode[] {
    var projectsConverted: FileNode[] = [];
    if (template != undefined) {
      var fileType: FileNodeType = convertNumberToFileType(template.projectType)
      if (template.files.length > 0) {
        template.files.forEach(pjFile => {
          var checkpoint = pjFile.checkpoints.reduce((r, o) => r.created < o.created ? r : o);
          var childFile = new FileNode(pjFile.fileName, fileType, checkpoint.code);
          childFile.fileId = pjFile.id;
          childFile.projectid = template.id;
          projectsConverted.push(childFile);
        });
      }
    }
    return projectsConverted;
  }