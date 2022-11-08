import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class KeepsService {
  async getAllKeeps(){
    const res = await api.get('api/keeps')
// NOTE Have to set up appstate and model before I can data dump
    logger.log('[get all keeps]', res.data)
    AppState.keeps = res.data.map(k => new Keep(k))
  }

  async getProfileKeeps(profileId){
    const res = await api.get('api/profiles/' + profileId + '/keeps')
    AppState.keeps = res.data.map((k) => new Keep(k))

  }
  async removeKeep(id){
    api.delete('api/keeps/' + id)
    AppState.keeps = AppState.keeps.filter(l=> k.id != id)
  }

}
export const keepsService = new KeepsService()