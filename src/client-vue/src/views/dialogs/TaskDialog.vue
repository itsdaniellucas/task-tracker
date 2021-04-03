<template>
    <v-dialog v-model="visible"
                persistent
                max-width="500">
      <template v-slot:activator="{ on, attrs }">
        <v-btn v-if="isAdd" 
                fab
                small
                :color="disabled ? 'secondary' : 'primary'"
                v-on="on"
                v-bind="attrs"
                :disabled="disabled">
            <v-icon dark>
              mdi-plus
            </v-icon>
        </v-btn>
        <v-btn v-if="isEdit && isVisible"
                color="secondary"
                fab
                x-small
                class="ml-n2 mr-2"
                v-on="on"
                v-bind="attrs"
                :disabled="disabled">
            <v-icon>mdi-pencil</v-icon>
        </v-btn>
      </template>
      <v-card>
        <v-card-title class="headline">
            <span v-if="isAdd">New Task</span>
            <span v-if="isEdit">Modify Task</span>
        </v-card-title>
        <v-card-text class="mb-0 pb-0">
            <v-divider />
            <v-row class="mt-2">
                <v-col cols="12" class="pb-0 my-0">
                    <v-text-field v-model="internalTask.name"
                                    label="Task"
                                    placeholder="Name"
                                    prepend-inner-icon="mdi-card-account-details"
                                    outlined
                                    counter
                                    maxlength="50">
                    </v-text-field>
                </v-col>
            </v-row>
            <v-row>
                <v-col cols="12" class="py-0 my-0">
                    <v-textarea v-model="internalTask.description"
                                    label="Description"
                                    placeholder="Details of the task"
                                    prepend-inner-icon="mdi-information"
                                    outlined
                                    maxlength="250"
                                    counter
                                    no-resize
                                    rows="5">
                    </v-textarea>
                </v-col>
            </v-row>
            <v-row>
                <v-col cols="6" class="py-0 my-0">
                    <v-text-field v-model="internalTask.actualTime"
                                    label="Actual"
                                    placeholder="Time (Hours)"
                                    prepend-inner-icon="mdi-timer-sand-full"
                                    type="number"
                                    outlined>
                    </v-text-field>
                </v-col>
                <v-col cols="6" class="py-0 my-0">
                    <v-text-field v-model="internalTask.expectedTime"
                                    label="Expected"
                                    placeholder="Time (Hours)"
                                    prepend-inner-icon="mdi-timer-sand-empty"
                                    type="number"
                                    outlined>
                    </v-text-field>
                </v-col>
            </v-row>
            <v-row>
                <v-col cols="6" class="py-0 my-0">
                    <v-select
                        :items="state.classifications.values"
                        :item-text="'name'"
                        :item-value="'id'"
                        v-model="internalTask.classificationId"
                        label="Classification"
                        placeholder="Column"
                        prepend-inner-icon="mdi-format-list-bulleted-type"
                        hide-details
                        outlined>
                    </v-select>
                </v-col>
                <v-col cols="6" class="py-0 my-0">
                    <v-switch v-model="internalTask.isCompleted"
                        color="primary"
                        hide-details
                        label="Done?" />
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
    import state from '@/state'
    import utility from '@/utility'

    export default {
        name: 'TaskDialog',

        props: {
            isVisible: {
                type: Boolean,
                default: true
            },
            task: {
                type: Object,
                default: () => ({}),
            },
            mode: {
                type: String,
                default: 'add'
            },
            classification: {
                type: Object,
                default: () => ({})
            },
            disabled: {
                type: Boolean,
                default: false,
            }
        },

        computed: {
            isAdd() {
                return this.mode == 'add'
            },
            isEdit() {
                return this.mode == 'edit';
            }
        },

        data: () => ({
            visible: false,
            internalTask: {},
            state,
        }),

        watch: {
            task(newVal) {
                this.initialize(newVal);
            },
            visible() {
                this.initialize(this.task);
            }
        },

        methods: {
            initialize(newTask) {
                if(!newTask || utility.isEmptyObject(newTask)) {
                    this.internalTask = {
                        id: 0,
                        name: '',
                        description: '',
                        isCompleted: false,
                        classificationId: 1,
                        actualTime: 0,
                        expectedTime: 0,
                        sprintId: 0,
                    };

                    if(this.classification && this.isAdd) {
                        this.internalTask.classificationId = this.classification.id;
                        this.internalTask.sprintId = state.sprints.current;
                    }
                    
                } else {
                    this.internalTask = {  ...newTask };
                }
            },
            onSave() {
                this.$emit('on-save', {
                    task: { ...this.internalTask },
                    mode: this.mode,
                });
                this.onClose();
            },
            onClose() {
                this.initialize({});
                this.visible = false;
            }
        },

        mounted() {
            this.initialize(this.task);
        }
    }
</script>

<style scoped>

</style>