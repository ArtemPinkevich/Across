export enum LocalStorageKey {
    ACCESS_TOKEN = 'ACCESS_TOKEN',
    REFRESH_TOKEN = 'REFRESH_TOKEN',
}


export function getValueFromLocalStorege(key: LocalStorageKey) {
    return localStorage.getItem(key)
}

export function setValueToLocalStorege(key: LocalStorageKey, value: string) {
    localStorage.setItem(key, value)
}