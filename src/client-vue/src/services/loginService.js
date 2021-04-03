import Vue from 'vue'
import AjaxService from '@/services/ajaxService'
import StorageService from '@/services/storageService'
import state from '@/state'

const loginService = new Vue({

    data: () => ({
        controller: 'Login',
    }),

    methods: {
        login(model) {
            return AjaxService.post({
                url: `/${this.controller}/Authenticate`, 
                data: model,
                loadingMsg: 'Logging in...',
                successMsg: 'You have successfully logged in.',
                errorMsg: 'Login failed.'
            }).then((data) => {
                if(data) {
                    if(data.Error) {
                        state.alert.error(data.Error);
                    } else {
                        StorageService.setTokenExpiration(data.TokenExpiration);
                        StorageService.setToken(data.Token);
                        StorageService.setUser(data.User);
                        state.user.setUser(data.User);
                    }

                    return data.User;
                } else {
                    return data;
                }
            });
        },
        logout() {
            StorageService.setTokenExpiration();
            StorageService.setToken();
            StorageService.setUser();
            state.user.setUser(null);
            state.alert.success('You have successfully logged out.');
        },
        getUser(skipFetch = false) {
            let user = StorageService.getUser();

            if(user) {
                return Promise.resolve(user);
            } else if(!user && !skipFetch) {
                return AjaxService.getSimple(`/${this.controller}/GetUser`).then((data) => {
                    StorageService.setUser(data);
                    return data;
                });
            }

            return Promise.resolve(null);
        },
        isLoggedIn() {
            return !!StorageService.getToken() && !!StorageService.getTokenExpiration() && (new Date().getTime() < StorageService.getTokenExpiration());
        },
    }
});

export default loginService