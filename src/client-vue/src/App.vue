<template>
  <v-app id="inspire">
    <v-app-bar
      app
      color="white"
      flat>
      <v-container class="py-0 fill-height">
        <LoginDialog @on-login="onLogin" @on-logout="onLogout" />

        <v-btn
          v-for="link in links"
          :key="link.name"
          text
          @click="$router.push({ name: link.name }).catch(_ => _)">
          <v-icon left>{{ link.icon }}</v-icon>
          {{ link.name }}
        </v-btn>

        <v-spacer></v-spacer>

        <v-responsive max-width="260">
          <v-select
            :items="state.sprints.values"
            :item-text="'name'"
            :item-value="'id'"
            @change="onSprintChange"
            :value="state.sprints.current"
            label="Sprint"
            prepend-icon="mdi-run"
            hide-details
            single-line
            class="ma-2"
          ></v-select>
        </v-responsive>

        <SprintDialog :disabled="!state.user.isAdmin" @on-save="onSprintSave" />
      </v-container>
    </v-app-bar>
  
    <v-main class="grey lighten-3">
      <v-progress-linear indeterminate color="cyan" v-show="state.isFetching" />
      <v-container>
        <router-view />
        <AlertBox />
      </v-container>
    </v-main>
  </v-app>
</template>

<script>

import SprintDialog from '@/views/dialogs/SprintDialog'
import LoginDialog from '@/views/dialogs/LoginDialog'
import AlertBox from '@/views/content/AlertBox'
import state from '@/state'
import SignalRService from '@/services/signalRService'
import LoginService from '@/services/loginService'


export default {
  name: 'App',

  components: {
    SprintDialog,
    LoginDialog,
    AlertBox
  },

  data: () => ({
    state,
    links: [
      { name: 'Dashboard', icon: 'mdi-view-dashboard-variant'},
      { name: 'Statistics', icon: 'mdi-chart-areaspline'},
    ],
  }),

  methods: {
    onSprintChange(value) {
      state.sprints.setCurrentSprint(value);
      state.tasks.fetch(state.sprints.current);
    },
    onSprintSave($event) {
      state.sprints.addSprint($event.sprint).then(() => {
        return state.sprints.fetch();
      });
    },
    onLogin($event) {
      LoginService.login({
        Username: $event.login.username,
        Password: $event.login.password,
      })
    },
    onLogout() {
      LoginService.logout();
    },
  },

  created() {
    SignalRService.initialize();
    LoginService.getUser(true).then(u => {
      state.user.setUser(u);
    });


    if(state.classifications.values.length == 0) {
      state.classifications.fetch();
    }

    if(state.sprints.values.length == 0) {
      state.sprints.fetch().then(() => {
        if(state.sprints.values.length != 0) {
          state.sprints.setCurrentSprint(state.sprints.values[0].id);
          return state.tasks.fetch(state.sprints.current);
        }
      });
    } else {
      state.sprints.setCurrentSprint(state.sprints.values[0].id);
    }
  }
};
</script>


<style>
  html {
    overflow-y: auto !important;
  }
</style>