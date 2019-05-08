export class CreateTestResultModel {
  userName: string;
  urlId: number;

  constructor(userName: string, urlId: number) {
    this.userName = userName;
    this.urlId = urlId;
  }
}
