<template>
    <v-dialog v-model="visible"
                persistent
                max-width="350">
        <template v-slot:activator="{ on, attrs }">
            <v-btn fab
                    small
                    :color="disabled ? 'secondary' : 'primary'"
                    v-on="on"
                    v-bind="attrs"
                    :disabled="disabled">
                <v-icon dark>
                    mdi-plus
                </v-icon>
            </v-btn>
        </template>
        <v-card>
            <v-card-title class="headline">
                <span>New Sprint</span>
            </v-card-title>
            <v-card-text class="mb-0 pb-0">
                <v-divider />
                <v-row class="mt-2">
                    <v-col cols="12">
                        <v-text-field v-model="sprintName"
                                        label="Sprint"
                                        placeholder="Name"
                                        prepend-inner-icon="mdi-card-account-details"
                                        outlined
                                        counter
                                        maxlength="50">
                        </v-text-field>
                    </v-col>
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
                        @click="onSave">
                    Save
                </v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>

<script>
    export default {
        name: 'SprintDialog',

        props: {
            disabled: {
                type: Boolean,
                default: false,
            }
        },

        data: () => ({
            visible: false,
            sprintName: '',
        }),

        methods: {
            initialize() {
                this.sprintName = '';
            },

            onSave() {
                this.$emit('on-save', {
                    sprint: this.sprintName
                });
                this.onClose();
            },

            onClose() {
                this.initialize();
                this.visible = false;
            }
        },

        mounted() {
            this.initialize();
        },
    }
</script>

<style scoped>

</style>