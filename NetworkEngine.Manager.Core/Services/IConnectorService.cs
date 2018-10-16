using System.Collections.Generic;
using System.Threading.Tasks;
using NetworkEngine.Manager.Core.DataModels.Connector;

namespace NetworkEngine.Manager.Core.Services
{
    public interface IConnectorService
    {
        /// <summary>
        /// Direct access to the internal connector
        /// </summary>
        Connector.Connector Connector { get; }

        /// <summary>
        /// Initializes the connector
        /// </summary>
        /// <returns></returns>
        Task Init();

        #region Session / Tunnel Methods

        /// <summary>
        /// Gets a list of all active sessions
        /// </summary>
        /// <returns></returns>
        Task<List<Session>> GetSessions();

        /// <summary>
        /// Opens a tunnel
        /// </summary>
        /// <param name="session">The session to tunnel to</param>
        /// <param name="key">Optional key for the tunnel</param>
        /// <exception cref="Exceptions.TunnelException">Thrown when a session does not support tunneling, or when a key is required</exception>
        /// <returns></returns>
        Task<Tunnel> OpenTunnel(Session session, string key = "");

        #endregion

        #region Scene Methods

        /// <summary>
        /// Gets all the scene nodes and their components
        /// </summary>
        /// <returns></returns>
        Task<List<Node>> GetScene();

        /// <summary>
        /// Resets the scene to the default scene
        /// </summary>
        /// <returns></returns>
        Task<StatusResponse> ResetScene();

        #endregion

        #region Scene Node Methods

        /// <summary>
        /// Adds a node to the scene
        /// </summary>
        /// <param name="name">The node name</param>
        /// <param name="parent">Optional parent node</param>
        /// <param name="components">Optional node components</param>
        /// <returns></returns>
        Task<Node> AddSceneNode(string name, Node parent = null, object components = null);

        /// <summary>
        /// Updates a node with the specified data
        /// </summary>
        /// <param name="node">The node to update</param>
        /// <param name="data">The data to change</param>
        /// <returns></returns>
        Task<StatusResponse> UpdateSceneNode(Node node, object data);

        /// <summary>
        /// Deletes the specified node from the scene
        /// </summary>
        /// <param name="node">The node to remove</param>
        /// <returns></returns>
        Task<StatusResponse> DeleteSceneNode(Node node);

        /// <summary>
        /// Finds a node in the scene
        /// </summary>
        /// <param name="name">The name of the node</param>
        /// <returns></returns>
        Task<List<Node>> FindSceneNode(string name);

        #endregion

    }
}
