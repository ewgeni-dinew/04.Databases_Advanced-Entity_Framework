namespace PhotoShare.Client.Core
{
    using PhotoShare.Client.Core.Commands;
    using PhotoShare.Models;
    using System;
    using System.Linq;

    public class CommandDispatcher
    {
        public string DispatchCommand(string[] commandParameters, Session session)
        {
            string command = commandParameters.First();
            string[] parameters = commandParameters.Skip(1).ToArray();
            int parametersCount = parameters.Length;

            string output = string.Empty;

            switch (command.ToLower())
            {
                case "login":
                    ValidateCommandParametersCount(command, parameters.Length, 2, true);
                    output = LoginCommand.Execute(parameters, session);
                    break;
                case "logout":
                    ValidateCommandParametersCount(command, parameters.Length, 0, true);
                    output = LogoutCommand.Execute(session);
                    break;
                case "registeruser":
                    ValidateCommandParametersCount(command, parameters.Length, 4, true);
                    output = RegisterUserCommand.Execute(parameters, session);
                    break;
                case "addtown":
                    ValidateCommandParametersCount(command, parameters.Length, 2, true);
                    output = AddTownCommand.Execute(parameters, session);
                    break;
                case "modifyuser":
                    ValidateCommandParametersCount(command, parameters.Length, 3, true);
                    output = ModifyUserCommand.Execute(parameters, session);
                    break;
                case "deleteuser":
                    ValidateCommandParametersCount(command, parameters.Length, 1, true);
                    output = DeleteUser.Execute(parameters, session);
                    break;
                case "addtag":
                    ValidateCommandParametersCount(command, parameters.Length, 1, true);
                    output = AddTagCommand.Execute(parameters, session);
                    break;
                case "createalbum":
                    ValidateCommandParametersCount(command, parameters.Length, 3, false);
                    output = CreateAlbumCommand.Execute(parameters, session);
                    break;
                case "addtagto":
                    ValidateCommandParametersCount(command, parameters.Length, 2, true);
                    output = AddTagToCommand.Execute(parameters, session);
                    break;
                case "makefriends":
                    ValidateCommandParametersCount(command, parameters.Length, 2, true);
                    output = AddFriendCommand.Execute(parameters, session);
                    break;
                case "acceptfriend":
                    ValidateCommandParametersCount(command, parameters.Length, 2, true);
                    output = AcceptFriendCommand.Execute(parameters, session);
                    break;
                case "listfriends":
                    ValidateCommandParametersCount(command, parameters.Length, 1, true);
                    output = PrintFriendsListCommand.Execute(parameters, session);
                    break;
                case "sharealbum":
                    ValidateCommandParametersCount(command, parameters.Length, 3, true);
                    output = ShareAlbumCommand.Execute(parameters, session);
                    break;
                case "uploadpicture":
                    ValidateCommandParametersCount(command, parameters.Length, 3, true);
                    output = UploadPictureCommand.Execute(parameters, session);
                    break;
                case "exit":
                    ValidateCommandParametersCount(command, parameters.Length, 0, true);
                    output = ExitCommand.Execute();
                    break;
                default:
                    throw new 
                        InvalidOperationException($"Command {command} not valid!");
            }

            return output;
        }

        private void ValidateCommandParametersCount(string commandName, int parametersCount, int neededCount, bool equalsOrBigger)
        {
            if((equalsOrBigger && parametersCount != neededCount) || (!equalsOrBigger && parametersCount < neededCount))
            {
                throw new ArgumentException($"Command {commandName} not valid! Parameters count must be {(equalsOrBigger ? "exactly" : "minimum")} {neededCount}!");
            }
            #region
            //if (equalsOrBigger)
            //{
            //    if (parametersCount != neededCount)
            //    {
            //        throw new ArgumentException($"Command {commandName} not valid! Parameters count must be {neededCount}!");
            //    }
            //}
            //else
            //{
            //    if (parametersCount < neededCount)
            //    {
            //        throw new ArgumentException($"Command {commandName} not valid! Parameters count must be {neededCount}!");
            //    }
            //}
            #endregion
        }
    }
}
