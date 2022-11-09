export class Vault{
  constructor(data){
    this.id = data.id
    this.name = data.name
    this.description = data.description
    this.isPrivate = data.isPrivate
    this.img = data.img
    this.creatorId = data.creatorId
    this.creator = data.creator
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt

  }
}