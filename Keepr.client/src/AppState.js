import { reactive } from 'vue'

// NOTE AppState is a reactive object to contain app level data
export const AppState = reactive({
  user: {},
  /** @type {import('./models/Account.js').Account} */
  account: {},
  /** @type {import('./models/Keep.js').Keep[]} */
  keeps: [],
  /** @type {import('./models/Account.js').Account | null} */

  activeProfile: {},
  /** @type {import('./models/Keep.js').Keep|null} */
keep: null,
activeKeep: null,
vaults: [],
activeVault: {},
vaultKeeps: [],
myKeeps: []

})
