namespace Sarsoo.Terraform.MachineReadableUI.Constant;

public static partial class MruiConst
{
    public static class MessageCode
    {
        public static class Init
        {
            public static class General
            {
                /// <summary>
                /// indicates progress during initialisation of HCP Terraform (cloud backend)
                /// </summary>
                public const string InitTfCloud = "initializing_terraform_cloud_message";

                /// <summary>
                /// indicates progress during initialisation of a backend
                /// </summary>
                public const string InitBackend = "initializing_backend_message";

                /// <summary>
                /// describes an empty message; equivalent to a new line aiding formatting in terminal output
                /// </summary>
                public const string Empty = "empty_message";

                /// <summary>
                /// indicates that an empty directory was successfully initialised
                /// </summary>
                public const string OutputInitEmpty = "output_init_empty_message";

                /// <summary>
                /// indicates that a [not empty] directory was successfully initialised via CLI
                /// </summary>
                public const string OutputInitSuccess = "output_init_success_message";

                /// <summary>
                /// indicates that a [not empty] directory was successfully initialised via HCP Terraform
                /// </summary>
                public const string OutputInitSuccessCloud = "output_init_success_cloud_message";

                /// <summary>
                /// instructions for next steps after a directory was successfully initialised via CLI
                /// </summary>
                public const string OutputInitSuccessCli = "output_init_success_cli_message";

                /// <summary>
                /// instructions for next steps after a directory was successfully initialised via HCP Terraform
                /// </summary>
                public const string OutputInitSuccessCliCloud = "output_init_success_cli_cloud_message";
            }

            public static class ProviderInstall
            {
                /// <summary>
                /// indicates progress during installation of providers from configuration
                /// </summary>
                public const string InitFromConfig = "initializing_provider_plugin_from_config_message";

                /// <summary>
                /// indicates progress during installation of providers from state
                /// </summary>
                public const string InitFromState = "initializing_provider_plugin_from_state_message";

                /// <summary>
                /// indicates provider version reuse during state-based provider installation
                /// </summary>
                public const string ReusingVersion = "reusing_version_during_state_provider_init";

                /// <summary>
                /// indicates a provider that is already installed during installation
                /// </summary>
                public const string AlreadyInstalled = "provider_already_installed_message";

                /// <summary>
                /// indicates a built-in provider in use during installation
                /// </summary>
                public const string Builtin = "built_in_provider_available_message";

                /// <summary>
                /// indicates that a provider is being installed (from a remote location)
                /// </summary>
                public const string Installing = "installing_provider_message";

                /// <summary>
                /// describes that a new lock file was created for installed providers
                /// </summary>
                public const string LockInfo = "lock_info";

                /// <summary>
                /// describes that the lock file was changed (e.g. as a result of provider installation or upgrade)
                /// </summary>
                public const string DependencyLockChange = "dependencies_lock_changes_info";
            }

            public static class ModuleInstall
            {
                /// <summary>
                /// indicates progress during module contents copying when -from-module flag is used
                /// </summary>
                public const string CopyingConfig = "copying_configuration_message";

                /// <summary>
                /// indicates progress during module upgrading when -upgrade flag is used
                /// </summary>
                public const string Upgrading = "upgrading_modules_message";

                /// <summary>
                /// indicates progress during installation of modules
                /// </summary>
                public const string Initializing = "initializing_modules_message";
            }
        }

        public static class OperationResults
        {
            /// <summary>
            /// describes a detected change to a single resource made outside of Terraform
            /// </summary>
            public const string Drift = "resource_drift";

            /// <summary>
            /// describes a planned change to a single resource
            /// </summary>
            public const string PlannedChange = "planned_change";

            /// <summary>
            /// summary of all planned or applied changes
            /// </summary>
            public const string ChangeSummary = "change_summary";

            /// <summary>
            /// list of all root module outputs
            /// </summary>
            public const string Outputs = "outputs";
        }

        public static class ResourceProgress
        {
            /// <summary>
            /// sequence of messages indicating progress of a single resource through apply
            /// </summary>
            public static class Apply
            {
                public const string Start = "apply_start";
                public const string Progress = "apply_progress";
                public const string Complete = "apply_complete";
                public const string Errored = "apply_errored";
            }

            /// <summary>
            /// sequence of messages indicating progress of a single provisioner step
            /// </summary>
            public static class Provision
            {
                public const string Start = "provision_start";
                public const string Progress = "provision_progress";
                public const string Complete = "provision_complete";
                public const string Errored = "provision_errored";
            }

            public static class Ephemeral
            {
                public const string Start = "ephemeral_op_start";
                public const string Progress = "ephemeral_op_progress";
                public const string Complete = "ephemeral_op_complete";
                public const string Errored = "ephemeral_op_errored";
            }

            /// <summary>
            /// sequence of messages indicating progress of a single resource through refresh
            /// </summary>
            public static class Refresh
            {
                public const string Start = "refresh_start";
                public const string Complete = "refresh_complete";
            }
        }

        public static class TestResults
        {
            /// <summary>
            /// describes the test operation that Terraform executes
            /// </summary>
            public const string Abstract = "test_abstract";

            /// <summary>
            /// describes the status of a completed test file
            /// </summary>
            public const string File = "test_file";

            /// <summary>
            /// describes the status of a completed run block within a test file
            /// </summary>
            public const string Run = "test_run";

            /// <summary>
            /// describes the result of the test cleanup after a completed test file
            /// </summary>
            public const string Cleanup = "test_cleanup";

            /// <summary>
            /// describes the overall status of the completed test operation
            /// </summary>
            public const string Summary = "test_summary";

            /// <summary>
            /// describes the output of a given run block when command = plan
            /// </summary>
            public const string Plan = "test_plan";

            /// <summary>
            /// describes the output of a given run block when command = apply
            /// </summary>
            public const string State = "test_state";

            /// <summary>
            /// describes the result of an interrupted test operation
            /// </summary>
            public const string Interrupt = "test_interrupt";
        }
    }
}