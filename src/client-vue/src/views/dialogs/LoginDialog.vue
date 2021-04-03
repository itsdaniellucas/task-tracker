<template>
    <v-dialog v-model="visible"
                persistent
                max-width="350">
      <template v-slot:activator="{ on, attrs }">
          <v-btn fab
                dark
                small
                :color="state.user.isLoggedIn ? 'primary' : 'grey darken-1'"
                class="mr-10"
                v-on="on"
                :bind="attrs">
            <v-icon dark>mdi-account-circle</v-icon>
          </v-btn>
        
      </template>
      <v-card>
        <v-card-title class="headline">
            <span v-if="!state.user.isLoggedIn">Login</span>
            <span v-else>Logout</span>
        </v-card-title>
        <v-card-text :class="{ 'mb-0 pb-0': state.user.isLoggedIn }">
            <v-divider />
            <v-row v-if="!state.user.isLoggedIn" class="mt-2">
                <v-col cols="12" class="pb-0 my-0">
                    <v-text-field v-model="login.username" 
                                    label="Username"
                                    prepend-inner-icon="mdi-account-circle"
                                    outlined>
                    </v-text-field>
                </v-col>
            </v-row>
            <v-row v-if="!state.user.isLoggedIn">
                <v-col cols="12" class="py-0 my-0">
                    <v-text-field v-model="login.password" 
                                    label="Password"
                                    :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
                                    prepend-inner-icon="mdi-lock"
                                    :type="showPassword ? 'text' : 'password'"
                                    @click:append="showPassword = !showPassword"
                                    outlined>
                    </v-text-field>
                </v-col>
            </v-row>
            <v-row v-if="state.user.isLoggedIn" class="pa-5">
                <h3>Are you sure you want to logout?</h3>
            </v-row>
        </v-card-text>
        <v-card-actions class="mt-0 pt-0">
          <v-spacer></v-spacer>
          <v-btn color="error darken-1"
                text
                @click="onClose">
            Cancel
          </v-btn>
          <v-btn color="success darken-1"
                text
                @click="onLogin">
            <span v-if="!state.user.isLoggedIn">Login</span>
            <span v-else>Logout</span>
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
</template>

<script>
    import state from '@/state'

    export default {
        name: 'LoginDialog',

        data: () => ({
            visible: false,
            showPassword: false,
            login: {
                username: '',
                password: '',
            },
            state,
        }),

        methods: {
            initialize() {
                this.login = {
                    username: '',
                    password: '',
                }
            },

            onLogin() {
                if(!state.user.isLoggedIn) {
                    this.$emit('on-login', {
                        login: { ...this.login },
                    });
                } else {
                    this.$emit('on-logout');
                }
                this.onClose();
            },

            onClose() {
                this.initialize();
                this.visible = false;
            }
        },

        mounted() {
            this.initialize();
        }
    }
</script>

<style scoped>

</style>