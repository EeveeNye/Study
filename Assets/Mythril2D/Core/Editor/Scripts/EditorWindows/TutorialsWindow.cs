using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Gyvr.Mythril2D
{
    public interface ITutorial
    {
        public string Title { get; }
        public string Description { get; }
        public string[] Paragraphs { get; }
    }

    public class TutorialGettingStarted : ITutorial
    {
        public string Title => "Getting Started";
        public string Description => "本教程将介绍Mythril2D的基础知识以及如何开始使用。";

        public string[] Paragraphs => new string[]
        {
            "<b>什么是Mythril2D？</b>\nMythril2D是一个工具包，旨在帮助游戏开发者轻松快速地创建2D动作角色扮演游戏。ScriptableObjects是Mythril2D的关键组件，使用户可以创建自己的故事、角色、物品等，而无需编写代码。通过消除这一要求，Mythril2D使开发者能够专注于设计和实验他们的游戏，无论他们的编程经验如何。借助Mythril2D，无论您是经验丰富的程序员还是非技术爱好者，都可以轻松高效地创建2D动作角色扮演游戏。",
            "<b>1. 初学者模式：</b>对于初学者和从事较小型游戏制作的人来说，使用Mythril2D创建游戏的最简单、最快速的方法是利用演示游戏中包含的预制资源。这些资源可以重复使用，并可以在相同的艺术风格下补充其他资源，这些资源可以在创建者页面上找到（请参阅演示游戏中使用的每个第三方资源的许可证文档）。通过利用这些资源，用户可以简化游戏开发流程，专注于将自己独特的想法和愿景变为现实。",
            "<b>2. 高级模式：</b>使用Mythril2D启动您的游戏制作可以使用演示游戏作为基础，并将其中的资源替换为您自己的资源。然而，需要注意的是，角色扮演游戏需要大量的资源，包括精灵、动画、音乐和音效。这种方法适用于有经验的用户从事中型游戏制作，他们拥有创建或获取必要资源的资源和专业知识。",
            "<b>3. 专家模式：</b>具有对Mythril2D有深入了解的专业用户，从头开始构建他们的游戏，使用工具包提供的核心构建块。这种方法需要对Mythril2D有深入了解的专业知识，仅建议那些在游戏开发和设计方面拥有重要专业知识的人使用。通过利用工具包提供的全套功能，有经验的用户可以构建复杂而复杂的游戏，突破Mythril2D的局限性。",
            "<b>准备好开始了吗？</b>\nMythril2D可能因其规模和复杂性而显得令人畏惧，但不要气馁。要开始，我建议您探索可用的教程和演示游戏。演示游戏是了解如何使用Mythril2D创建独特角色扮演游戏的绝佳资源。仔细查看演示角色、技能、地图和其他资源，了解如何调整和自定义它们以适应您自己的愿景和游戏设计。通过坚持和实践，您可以掌握Mythril2D的工具和功能，创建您梦寐以求的游戏。",
            "<b><i>请记住，ScriptableObjects对于Mythril2D至关重要。如果您对它们不熟悉，请在开始使用Mythril2D创建自己的游戏之前查阅一些Unity教程。</i></b>"
        };
    }


    public class TutorialScenes : ITutorial
    {
        public string Title => "Scenes";
        public string Description => "在本教程中，我们将学习Mythril2D场景系统以及如何创建自己的地图。";

        public string[] Paragraphs => new string[]
        {
            "<b>场景系统</b>\nMythril2D使用Unity的增量场景加载和异步加载。有两个主要的场景可以加载：主菜单和游戏场景。这两个场景是独立的场景，包含了所有的游戏逻辑、用户界面和系统。然而，仅加载游戏场景有点无聊，你可能会发现自己处于一个空的世界中！为了让你的游戏场景更有趣，了解更多关于地图的内容吧！",
            "<b>地图</b>\n地图可以被看作是Mythril2D加载在游戏场景之上的一个层级。它通常包含关卡设计元素，如装饰物、游戏组件、角色和音乐。相对于游戏场景，你会花费大部分时间来创建地图，而游戏场景则大部分保持不变。地图可以从SaveSystem（参见游戏系统）加载，当加载保存文件时，或者通过传送门（参见传送门）加载，当玩家与之交互时（例如进入一间房子）。",
            "<b>地图的测试</b>\n如前所述，只有主菜单和游戏场景是真正的独立场景。你的地图应该始终作为游戏场景的一个层级来使用。这意味着默认情况下，在编辑地图时你无法点击播放按钮进行测试。然而，通过向你的地图添加PlaytestManager（在演示项目中的PlaytestManager预制体中查看），你可以要求Mythril2D在你点击播放时加载游戏场景！这样可以加快地图的迭代速度，无需在游戏场景和地图之间来回切换。PlaytestManager还允许你创建具有独特保存文件的场景，这样你就可以使用不同的参数（例如不同的角色、物品、任务或游戏标记）来测试你的地图。",
        };
    }

    public class TutorialStats : ITutorial
    {
        public string Title => "Stats";
        public string Description => "在本教程中，我们将学习Mythril2D的属性系统。";

        public string[] Paragraphs => new string[]
        {
            "在Mythril2D中，角色的属性由几个特征组成：\n" +
            "<b>- 生命值：</b>决定角色在被击败之前能够承受的伤害量。提高此属性可以增加整体生存能力。\n" +
            "<b>- 物理攻击：</b>决定物理攻击（如剑击或箭射）造成的伤害量。\n" +
            "<b>- 魔法攻击：</b>影响魔法法术和技能的效果，允许操纵元素和施放强大的法术。\n" +
            "<b>- 物理防御：</b>决定从物理攻击中吸收的伤害量，减轻刀剑和箭矢带来的伤害。\n" +
            "<b>- 魔法防御：</b>影响角色对魔法攻击的抵抗力，减少来自法术的伤害。\n" +
            "<b>- 敏捷度：</b>增加闪避攻击的机会，降低错过攻击的机会。\n" +
            "<b>- 幸运：</b>增加暴击的机会。\n" +
            "增加这些属性可以让玩家创建与他们的游戏风格相匹配的独特角色。",
            "<b>自定义属性命名</b>\n每个属性可以直接在编辑器中通过GameConfig（参见游戏管理器）进行重命名。",
            "<b>自定义伤害计算</b>\n可以通过编辑DamageSolver类的代码来自定义游戏中的伤害计算。",
            "<b>等级</b>\n最大等级默认设置为20。您可以通过编辑Stats类中的\"MaxLevel\"来更改这个值（参见Stats.cs）。",
        };
    }


    public class TutorialCharacterCreation : ITutorial
    {
        public string Title => "Character Creation";
        public string Description => "在本教程中，我们将学习如何为游戏创建角色。";

        public string[] Paragraphs => new string[]
        {
            "<b>CharacterSheet <i>(创建 > Mythril2D > Characters > ...)</i></b>\nCharacterSheet是一个ScriptableObject，用于提供特定角色表格的基础，如HeroSheet、MonsterSheet和NPCSheet。创建角色表格是构建任何游戏角色的第一步。",
            "<b>SpriteLibrary</b>\n角色使用SpriteLibrary来确定它们的外观。在演示游戏中，SpriteLibrary包含角色手部的动画帧和视觉效果。不同的角色通常使用不同的SpriteLibrary。要填充新的SpriteLibrary，您可能需要导入一个精灵表或使用现有的精灵表。请注意，在演示游戏中，每个角色的SpriteLibrary使用Sprite Libraries/SLIB_Default作为它们的主要库，以保持一致的内容结构。",
            "<b>生成您的角色</b>\n一旦您拥有了角色表格（HeroSheet、MonsterSheet或NPCSheet）和角色的SpriteLibrary，就可以考虑将它生成到地图中！建议为每个角色创建一个预制体。在演示游戏中，您可以在Prefabs/Characters/下找到每种角色类型（0_Hero_Base、0_Monster_Base、0_NPC_Base）的基础预制体。从其中一个基础预制体创建一个预制体变种是创建新角色的便捷方式。然后，您可以编辑预制体变种，使用您的SpriteLibrary并根据需要更改角色设置。您可以在相应的教程中了解有关英雄、怪物和NPC的具体信息。"
        };
    }

    public class TutorialHeroes : ITutorial
    {
        public string Title => "Heroes";
        public string Description => "在本教程中，我们将学习更多关于英雄的内容。";

        public string[] Paragraphs => new string[]
        {
            "英雄是可玩的角色，通过PlayerController进行控制。",
            "<b>HeroSheet <i>(创建 > Mythril2D > Characters > HeroSheet)</i></b>\nHeroSheet允许您定义英雄的属性点获取方式、基础属性和技能。",
            "<b>PlayerController <i>(MonoBehaviour)</i></b>\nPlayerController组件与InputSystem（参见游戏系统）交互，读取和解释输入，并将其转化为英雄的移动和技能释放。"
        };
    }

    public class TutorialNPCs : ITutorial
    {
        public string Title => "NPCs";
        public string Description => "在本教程中，我们将学习更多关于NPC的内容。";

        public string[] Paragraphs => new string[]
        {
            "NPC是玩家可以与之互动的角色。无论您想要设置商店、旅馆、对话还是给予玩家任务，NPC都是一个好选择。通过创建与玩家之间有趣的互动，您可以为游戏创建独特的角色并增加故事的深度。",
            "<b>NPCSheet <i>(创建 > Mythril2D > Characters > NPCSheet)</i></b>\nNPCSheet是指向特定角色的简单方式。在视频游戏中，同一个NPC（一个NPCSheet）可能出现在不同的位置。该表格用于标识角色，因此如果一个任务要求与该角色互动，则角色特定的GameObject的位置并不重要。",
            "<b>互动</b>\n一旦创建了NPC（参见Characters），选择该角色以在检查器中显示其组件。找到NPC组件。在该组件下，您将找到\"Interactions\"部分，它允许您设置按优先级执行的互动。默认情况下，0_NPC_Base预制体将包含NPCDialogue和NPCQuestInteractor组件，其中NPCQuestInteractor优先级高于NPCDialogue（例如，如果此角色有一个任务可提供，则任务互动将执行，而不是NPCDialogue中的任何其他对话）。您可以添加其他互动，例如NPCShop和NPCInn，用于您的NPC。NPCQuestInteractor通常应放在互动列表的第一位。请注意，您可以在单个NPC上添加任意多个NPC互动（甚至是相同类型的互动）。",
            "<b>NPCDialogue <i>(MonoBehaviour)</i></b>\n当此组件添加到NPC并在其互动列表中引用时，NPCDialogue将允许该角色与玩家对话。它可以包含多个序列，按顺序（从上到下）进行评估，直到满足某个序列的条件为止。您可以查看演示游戏中的NPC Yosbar（Prefabs/Characters/NPCs/Yosbar）以查看如果特定任务任务处于活动状态，将执行对话序列的示例。",
            "<b>NPCQuestInteractor <i>(MonoBehaviour)</i></b>\n当此组件添加到NPC并在其互动列表中引用时，NPCQuestInteractor将允许该角色提供和提供有关任务的提示，并向该角色报告任务的完成情况。",
            "<b>NPCShop <i>(MonoBehaviour)</i></b>\n当此组件添加到NPC并在其互动列表中引用时，NPCShop将给玩家提供购买物品的选项。",
            "<b>NPCInn <i>(MonoBehaviour)</i></b>\n当此组件添加到NPC并在其互动列表中引用时，NPCInn将给玩家提供休息的选项，以换取一定的金钱。",
        };
    }

    public class TutorialMonsters : ITutorial
    {
        public string Title => "Monsters";
        public string Description => "在本教程中，我们将学习更多关于怪物的内容。";

        public string[] Paragraphs => new string[]
        {
            "怪物是通常使用AIController进行控制的角色。",
            "<b>MonsterSheet <i>(创建 > Mythril2D > Characters > MonsterSheet)</i></b>\nMonsterSheet可以定义怪物的等级演化方式、可用技能以及击败后的奖励（金钱、经验、物品等）。",
            "<b>AIController <i>(MonoBehaviour)</i></b>\nAIController组件根据角色周围的情况确定要执行的移动或释放的技能。您可以扩展AIController类以为怪物创建独特的行为。当前的AIController不支持多个技能或高级决策制定。您也可以使用状态机来替代AIController，具体取决于您的制作需求。"
        };
    }

    public class TutorialDialogues : ITutorial
    {
        public string Title => "Dialogues";

        public string Description =>
            "在本教程中，我们将学习有关Mythril2D对话系统的内容，以及如何创建复杂的对话序列。";

        public string[] Paragraphs => new string[]
        {
            "<b>DialogueSequence <i>(创建 > Mythril2D > Dialogues > DialogueSequence)</i></b>\n这个ScriptableObject是任何对话的入口点。",
            "<b>创建对话</b>\n创建对话时的第一步是创建一个DialogueSequence。",
            "<b>对话行</b>\nDialogueSequence包含一个可以显示在UI对话消息框中的行数组（DialogueNode）。在编写对话行时，您必须确保不会超出输出消息框的限制。",
            "<b>对话选项</b>\n在显示DialogueSequence中的所有行后，可以向玩家提供选择。如果没有提供选项（即选项数量 = 0），对话将在显示最后一行后终止。如果只设置一个选项，提供的DialogueSequence将自动播放当前序列之后的序列。在处理分支对话时，这非常方便，因为多个序列可以汇聚到同一个序列中。如果玩家可以选择多个选项，则可以通过填写第一个文本字段来为这些选项命名。最后，选项行末尾的下拉菜单允许您向消息记录添加特定字符串，这将在后面进行解释。",
            "<b>消息记录</b>\n对话选项可以填充消息记录，改变游戏的进程。某些脚本可能会播放对话序列并在执行结束时读取消息记录。每个选项可以向消息记录添加一个字符串。例如，用于启动任务的对话序列预计应包含\"接受\"或\"拒绝\"，这意味着您的序列可能应该在某个地方包含这些选项。演示游戏中的任务对话提供了使用消息记录的绝佳示例。您还可以使用自定义字符串填充消息记录，并在脚本中检索和解析这些字符串。这对于希望通过创建自定义脚本来扩展Mythril2D的高级用户非常有用。在大多数情况下，\"接受\"和\"拒绝\"消息将用于商店、旅馆或任务的对话序列。",
            "<b>完成时执行</b>\n在对话序列结束时，还可以执行ScriptableAction（参见ScriptableAction教程）来运行复杂的逻辑，例如在对话中恢复玩家的生命值、设置游戏标志或给玩家物品。",
        };
    }

    public class TutorialItems : ITutorial
    {
        public string Title => "Items";
        public string Description => "在本教程中，我们将学习有关Mythril2D物品的内容。";

        public string[] Paragraphs => new string[]
        {
            "<b>物品 <i>(创建 > Mythril2D > Items > Item)</i></b>\n在Mythril2D中，物品是可以获得（例如，作为任务奖励、战利品）和使用（例如，出售、使用）的对象。物品有一个价格，在商店（参见商店和旅馆）中可以购买或出售，并且默认情况下在使用时没有效果。为物品创建特殊效果需要通过脚本扩展Item类。在演示游戏中，该基类被扩展以创建生命和法力药水，以及装备。实际上，装备继承自Item。",
            "<b>装备 <i>(创建 > Mythril2D > Items > Equipment)</i></b>\nMythril2D中的装备是指英雄可以在特殊的背包槽中装备的物品。装备时，英雄将从装备设置中受益。当前，装备没有重量或职业的限制，任何英雄都可以装备任何装备。"
        };
    }

    public class TutorialAbilities : ITutorial
    {
        public string Title => "Abilities";
        public string Description => "在本教程中，我们将学习有关Mythril2D技能的内容。";

        public string[] Paragraphs => new string[]
        {
            "<b>技能表格 <i>(创建 > Mythril2D > Abilities > ...)</i></b>\n类似于角色表格，技能表格允许您定义技能的设置，例如名称、图标和描述。AbilitySheet类是任何类型的技能的基类，包括伤害技能（DamageAbilitySheet）、治疗技能（HealAbilitySheet）等。",
            "<b>技能预制体</b>\n对于每个创建的技能表格，您必须包含一个对应的预制体的引用，该预制体包含实际的视觉效果和游戏代码。该预制体必须包含与您正在创建的技能表格类型兼容的能力脚本（例如，DamageAbilitySheet、HealAbilitySheet）。例如，如果为技能创建了一个带有MeleeAttackAbility组件（MonoBehaviour）的预制体，则必须创建DamageAbilitySheet类型的表格，并在该表格中添加对预制体的引用。",
            "<b>将技能添加到角色</b>\n英雄和怪物可以通过各自的角色表格分配技能。当使用HeroSheet或MonsterSheet的角色生成时，将自动为每个角色已解锁的技能实例化能力预制体。"
        };
    }


    public class TutorialScriptableAction : ITutorial
    {
        public string Title => "Scriptable Actions";

        public string Description =>
            "在本教程中，我们将学习有关Mythril2D可脚本化行动的内容，以及如何使用它们。";

        public string[] Paragraphs => new string[]
        {
            "<b>ScriptableAction <i>(创建 > Mythril2D > ScriptableActions > ...)</i></b>\nScriptableAction是一个ScriptableObject，它包含游戏逻辑，例如奖励玩家物品、恢复玩家生命值、修改游戏标志、启动对话等等。在对话期间、完成任务或玩家与某物互动时执行游戏逻辑时，这些行动非常有用。将ScriptableAction视为游戏代码的引用。由于它是ScriptableObject，您可以在编辑器中使用它。通常，每种类型的脚本化行动（例如AddExperienceAction、SetGameFlagAction等）不需要创建多个，除非脚本化行动在检查器中公开设置（目前Mythril2D中没有这种情况，但对于自定义ScriptableAction可能存在这种情况）。",
            "<b>参数</b>\n每个ScriptableAction可能需要不同的输入参数。您可以选择在演示游戏中选择ScriptableAction并在检查器中显示它，以可视化所需的必需和可选参数，以及每个参数的文档。熟悉这些ScriptableAction对于使用Mythril2D非常重要。",
            "<b>ActionHandler <i>(类)</i></b>\nActionHandler是ScriptableAction和其参数的容器。例如，Quest ScriptableObject公开了一个ActionHandlers列表，用于在任务完成时执行一些游戏逻辑。ActionHandler中的第一个字段是要执行的ScriptableAction。一旦定义，参数列表将自动填充以反映ScriptableAction所期望的参数。某些ScriptableAction可能具有可选参数，例如AddOrRemoveItemAction，该参数接受一个Item引用（必需）以及数量（可选，默认设置为1）。您可以通过点击[+]添加可选参数或[-]删除它们。",
            "<b>ScriptableActionInvoker <i>(MonoBehaviour)</i></b>\n您可以将ScriptableActionInvoker添加到场景中，以在特定事件发生时使用特定参数调用ScriptableAction。您还可以定义多个必须满足的条件，以便调用ScriptableAction。ScriptableActionInvoker非常方便用于：\n- 游戏开始时播放背景音乐（在演示游戏的每个地图上使用）\n- 加载地图时播放对话（演示游戏中的Abandoned House使用）\n- 通过添加对话使游戏中的元素可交互（演示游戏中Abandoned House的梯子）\n- 以及更多！"
        };
    }

    public class TutorialQuests : ITutorial
    {
        public string Title => "Quests";
        public string Description => "在本教程中，我们将学习有关Mythril2D任务的内容。";

        public string[] Paragraphs => new string[]
        {
            "<b>Quest <i>(创建 > Mythril2D > Quests > Quest)</i></b>\n在Mythril2D中，任务是包含玩家必须执行的一系列任务的ScriptableObject，以获得奖励和/或执行特定的ScriptableAction。任务由NPC提供，并必须返回给相同或其他NPC。",
            "<b>Task <i>(创建 > Mythril2D > Quests > Tasks > ...)</i></b>\n任务也是ScriptableObject。Task类可以通过编程方式扩展以创建新的目标。默认情况下，Mythril2D包括两个任务：ItemTask和KillMonsterTask。",
            "<b>ItemTask <i>(创建 > Mythril2D > Quests > Tasks > ItemTask)</i></b>\nItemTask是一个任务，玩家需要收集特定物品。例如，如果任务要求玩家收集4个水晶球，则必须确保这些水晶球不能被处理；否则，玩家可能通过出售任务物品等方式使任务无法完成（注意：可以通过将物品的价格设置为0来使物品不可出售）。",
            "<b>KillMonsterTask <i>(创建 > Mythril2D > Quests > Tasks > KillMonsterTask)</i></b>\n此任务要求玩家击败特定怪物特定次数。"
        };
    }

    public class TutorialGameFlags : ITutorial
    {
        public string Title => "Game Flags";
        public string Description => "在本教程中，我们将学习有关Mythril2D游戏标志的内容。";

        public string[] Paragraphs => new string[]
        {
            "游戏标志是唯一的字符串标识符，可以设置或取消（布尔值），存储和保存在保存文件中，并用于检查游戏条件。例如，您可能希望在游戏中的某个操作中更改对话、怪物或可视效果。您可以通过在发生此操作时设置游戏标志并使用GameConditionChecker（请参阅游戏条件）检查此游戏标志是否已设置来实现此目的。",
        };
    }

    public class TutorialChests : ITutorial
    {
        public string Title => "Chests";
        public string Description => "在本教程中，我们将学习有关Mythril2D宝箱的内容。";

        public string[] Paragraphs => new string[]
        {
            "<b>Chest <i>(MonoBehaviour)</i></b>\n宝箱是一种游戏元素，可以包含一个物品，并处于打开或关闭状态。如果设置为单次使用，则宝箱需要提供一个唯一的游戏标志ID（请参阅游戏标志），用于标识宝箱并确保它不会被打开两次。",
            "<b>创建宝箱</b>\n要在地图中添加宝箱，您可以将演示游戏中的Chest预制体拖放到场景中，并在Chest脚本部分中更新其设置。",
        };
    }


    public class TutorialTeleporters : ITutorial
    {
        public string Title => "Teleporters";
        public string Description => "在本教程中，我们将学习有关Mythril2D传送门的内容。";

        public string[] Paragraphs => new string[]
        {
            "<b>Teleporter <i>(MonoBehaviour)</i></b>\n传送门是一种游戏元素，可以在同一地图内或从一个地图传送玩家到另一个地图。它对于任何地图转换（从室外到室内）或特定的游戏机制都是必要的。它可以设置为在接触时传送玩家，如果玩家朝特定方向移动。也可以设置在传送发生时播放声音。",
            "<b>创建传送门</b>\n要在地图中添加传送门，您可以将演示游戏中的传送门预制体拖放到场景中，并在传送门脚本部分中更新其设置。",
        };
    }

    public class TutorialShopsAndInns : ITutorial
    {
        public string Title => "Shops & Inns";
        public string Description => "在本教程中，我们将学习有关Mythril2D商店和旅馆的内容。";

        public string[] Paragraphs => new string[]
        {
            "<b>Shop <i>(创建 > Mythril2D > Shops > Shop)</i></b>\n\"商店是一个ScriptableObject，其中包含可供出售的物品列表。它还可以指定购买和销售倍增器以折扣或增加所购买或出售的物品价格。创建后，可以将商店实例传递给NPC，使用NPCShop交互（请参阅NPCs）。",
            "<b>Inn <i>(创建 > Mythril2D > Inns > Inn)</i></b>\n旅馆是一个ScriptableObject，定义了恢复多少生命值和法力值以及价格。创建后，可以将旅馆实例传递给NPC，使用NPCInn交互（请参阅NPCs）",
        };
    }

    public class TutorialAudio : ITutorial
    {
        public string Title => "Audio";
        public string Description => "在本教程中，我们将学习有关Mythril2D音频的内容。";

        public string[] Paragraphs => new string[]
        {
            "Mythril2D音频系统使用非确定性的基于请求的音频播放系统。这意味着要播放音乐或声音，脚本需要发出播放请求，其实现可能取决于当前设置。例如，如果脚本请求播放背景音乐，但没有背景音乐通道存在，声音将不会被播放，游戏将忽略它。",
            "<b>AudioChannel <i>(MonoBehaviour)</i></b>\n在Mythril2D中，音乐和声音通过音频通道播放。AudioChannel定义了如何播放声音，独占模式允许一次只能播放一个声音，多通道模式允许同时播放多个声音。每个AudioChannel都有自己的AudioSource，可以为每个通道设置不同的设置，例如音量和音调。例如，您可以将背景音乐的AudioChannel的音量设置为比其他通道低。这些AudioChannel在AudioSystem（请参阅游戏系统）中引用。",
            "<b>AudioClipResolver <i>(创建 > Mythril2D > Audio > AudioClipResolver)</i></b>\n在视频游戏中，常常会在多个位置引用相同的音频剪辑。然而，更新音频剪辑可能需要更改以前使用该剪辑的所有位置。为了解决这个问题并在处理多个剪辑时方便选择音频剪辑，Mythril2D引入了AudioClipResolver。这个ScriptableObject作为对一个或多个音频剪辑的引用，并使用选择的算法动态选择音频剪辑。它还指定了应该在哪个音频通道中播放剪辑。",
            "<b>AudioRegion <i>(MonoBehaviour)</i></b>\n在某些情况下，您可能希望在玩家进入地图的某个区域时播放特定的声音。为了实现这一点，您可以添加一个AudioRegion组件，当玩家进入其触发区域时，它将暂停背景音乐的播放，并在玩家离开时恢复播放。",
        };
    }

    public class TutorialDatabase : ITutorial
    {
        public string Title => "Database";
        public string Description => "在本教程中，我们将学习有关Mythril2D数据库的内容。";

        public string[] Paragraphs => new string[]
        {
            "<b>数据库是什么？</b>\nMythril2D数据库是一个方便的编辑器窗口，用于浏览ScriptableObject。您可以按类型搜索ScriptableObject，例如HeroSheet、Item或Quest。",
            "<b>如何打开数据库？</b>\n要访问数据库，请单击Unity工具栏中的\"Window\"选项，导航到\"Mythril2D\"，然后从下拉菜单中选择\"Database\"。",
        };
    }


    public class TutorialGameManager : ITutorial
    {
        public string Title => "Game Manager";
        public string Description => "在本教程中，我们将学习有关Mythril2D游戏管理器的内容。";

        public string[] Paragraphs => new string[]
        {
            "<b>游戏管理器 <i>(MonoBehaviour)</i></b>\n游戏管理器是一个需要添加到您的主菜单和游戏场景中的组件。它对于使用Mythril2D制作的任何游戏都是必不可少的。游戏管理器的作用是跟踪所有的游戏系统（参见游戏系统）并使用单例模式使它们公开可用。您可以将游戏管理器视为游戏逻辑的调解者。此外，游戏管理器持有对GameConfig实例的引用。GameConfig对象是您存储有关游戏的所有细节的地方，包括游戏玩法和术语等。",
            "<b>游戏配置 <i>(Create > Mythril2D > Game > GameConfig)</i></b>\nGameConfig ScriptableObject是一个对象，允许您完全配置游戏，例如是否可以产生暴击或未命中等，并配置所有的游戏术语，如货币、经验、等级等。这使您可以定义诸如\"Health\"属性在游戏中应如何称呼。也许您想将其称为\"HP\"或\"生命值\"。这取决于您的决策！",
        };
    }

    public class TutorialGameSystems : ITutorial
    {
        public string Title => "Game Systems";
        public string Description => "在本教程中，我们将学习有关Mythril2D游戏系统的内容。";

        public string[] Paragraphs => new string[]
        {
            "<b>什么是游戏系统？</b>\nMythril2D使用系统方法处理持久的游戏逻辑，例如库存系统、音频系统、日志系统等。系统通常在主菜单和游戏场景下的GameManager中设置，这意味着它们应该被实例化一次并保持活动状态，即使当前加载的地图发生变化。",
            "<b>InputSystem <i>(MonoBehaviour)</i></b>\n组织Unity的输入系统中设置的所有输入，并向其他脚本公开它们。它还处理输入锁定（例如在地图转换期间）和动作映射切换。",
            "<b>InventorySystem <i>(MonoBehaviour)</i></b>\n存储和处理玩家的库存，包括添加物品、装备物品或管理货币等操作。",
            "<b>JournalSystem <i>(MonoBehaviour)</i></b>\n存储和处理玩家的任务，包括开始任务和完成任务等操作。",
            "<b>NotificationSystem <i>(MonoBehaviour)</i></b>\n包含多个事件，其他系统或脚本可以订阅并调用以响应游戏事件，例如触发音频播放请求。",
            "<b>SaveSystem <i>(MonoBehaviour)</i></b>\n处理保存文件的创建、删除和加载。",
            "<b>GameStateSystem <i>(MonoBehaviour)</i></b>\n跟踪游戏状态，例如游戏进行中和UI界面，以允许系统和脚本根据当前游戏状态进行自适应。",
            "<b>GameFlagSystem <i>(MonoBehaviour)</i></b>\n存储和处理游戏标记的操作。脚本可以使用它来设置或检查特定的游戏标记（例如，一个箱子在打开后存储一个游戏标记，以确保它不会被再次打开）。",
            "<b>MapLoadingSystem <i>(MonoBehaviour)</i></b>\n处理地图的加载和卸载。如果设置为委派转换责任，则它将委派其操作，以允许外部系统或脚本（例如演示游戏中的UISceneTransition脚本）来启动和停止加载/卸载。",
            "<b>DialogueSystem <i>(MonoBehaviour)</i></b>\n存储对主对话通道的引用，该通道将处理所有对话逻辑。可以扩展它以支持多个通道（例如，在对话消息框中阻止对话和玩法上方的聊天气泡）。",
            "<b>PlayerSystem <i>(MonoBehaviour)</i></b>\n处理玩家预制体的实例化。默认情况下，PlayerSystem会生成设置在其\"Dummy Player Prefab\"字段中的预制体。请注意，保存系统将决定游戏应该使用哪个预制体作为玩家。当未加载保存文件时，通常会使用虚拟预制体。",
            "<b>PhysicsSystem <i>(MonoBehaviour)</i></b>\n更改Unity的物理系统，以在项目中以不同方式处理抛射物（目前此系统仅用于此目的，但可以根据您的项目需求进行扩展）。",
            "<b>AudioSystem <i>(MonoBehaviour)</i></b>\n处理用于播放AudioClipResolver的多个音频通道（请参阅音频）。您可以根据需要设置您的通道列表，最多可以使用5个不同的通道（背景音乐、背景声音、游戏音效、界面音效和其他杂项）。",
        };
    }

    public class TutorialNavigationCursors : ITutorial
    {
        public string Title => "Navigation Cursors";
        public string Description => "在本教程中，我们将学习有关Mythril2D导航光标的内容。";

        public string[] Paragraphs => new string[]
        {
            "<b>什么是导航光标？</b>\n导航光标是一个UI元素，跟随当前选定的UI元素，向玩家提供有关当前选择的元素的信息。例如，您可能希望在选择物品槽时在库存中显示一个框架。",
            "<b>UINavigationCursor <i>(MonoBehaviour)</i></b>\n当添加到屏幕空间UI中的GameObject中时，允许您显示一个图像，显示在选定的UI元素之上。",
            "<b>UINavigationCursorTarget <i>(MonoBehaviour)</i></b>\n将此组件添加到您希望导航光标对其做出反应的任何UI元素上。如果UI元素具有此组件，选择后UINavigationCursor将自动移动到该元素。",
            "<b>NavigationCursorStyle <i>(Create > Mythril2D > UI > NavigationCursorStyle)</i></b>\n此ScriptableObject定义导航光标在突出显示元素时应采用的样式。您可以为游戏中的每个不同UI元素创建任意数量的样式。",
        };
    }

    public class TutorialConditionalActivators : ITutorial
    {
        public string Title => "Conditional Activators";
        public string Description => "在本教程中，我们将学习有关Mythril2D条件激活器的内容。";

        public string[] Paragraphs => new string[]
        {
            "条件激活器是您可以在地图中添加的组件，用于在满足某些条件时启用或禁用特定的GameObject。",
            "<b>ConditionalChildrenActivator <i>(MonoBehaviour)</i></b>\n如果满足条件，则启用或禁用其所有子对象。",
            "<b>ConditionalReferencesActivator <i>(MonoBehaviour)</i></b>\n如果满足条件，则启用或禁用其所有引用。",
        };
    }


    public class TutorialTilemapLayers : ITutorial
    {
        public string Title => "Tilemap Layers";
        public string Description => "在本教程中，我们将学习有关Mythril2D瓦片图层的内容。";

        public string[] Paragraphs => new string[]
        {
            "在Mythril2D演示游戏中，环境使用Unity的瓦片图进行渲染。该演示中的地图设置为使用4个图层的组合。每个瓦片图层都有自己的瓦片图碰撞器，并使用自己的排序图层进行渲染（演示游戏使用的排序图层有：A、B、C和默认图层）。使用多个瓦片图层的目的是在环境中可视地表示深度。例如，您可能希望一个蘑菇瓦片（带有透明背景）显示在草地瓦片的上方，因此将草地瓦片放在A层，将蘑菇瓦片放在B层是个好主意。您可以自行决定将什么放入每个图层，以及想要使用多少个图层。玩家则在默认排序图层中渲染，因此默认图层中的任何瓦片与玩家之间的深度关系将基于这些元素的Y坐标。因此，对于玩家可以走在其后面的任何瓦片（例如树木、大石头、围栏或房屋），将其放在默认图层上，并让Unity通过比较瓦片和玩家的Y坐标来解决排序顺序是合理的。",
        };
    }

    public class TutorialsWindow : EditorWindow
    {
        public ITutorial[] m_tutorials = new ITutorial[]
        {
            new TutorialGettingStarted(), // 入门教程
            new TutorialScenes(), // 场景教程
            new TutorialTilemapLayers(), // 瓦片地图层教程
            new TutorialChests(), // 宝箱教程
            new TutorialTeleporters(), // 传送门教程
            new TutorialStats(), // 属性教程
            new TutorialCharacterCreation(), // 角色创建教程
            new TutorialHeroes(), // 英雄教程
            new TutorialNPCs(), // NPC教程
            new TutorialMonsters(), // 怪物教程
            new TutorialAbilities(), // 技能教程
            new TutorialShopsAndInns(), // 商店和客栈教程
            new TutorialItems(), // 物品教程
            new TutorialQuests(), // 任务教程
            new TutorialDialogues(), // 对话教程
            new TutorialAudio(), // 音频教程
            new TutorialScriptableAction(), // 脚本动作教程
            new TutorialGameFlags(), // 游戏标记教程
            new TutorialDatabase(), // 数据库教程
            new TutorialGameManager(), // 游戏管理器教程
            new TutorialGameSystems(), // 游戏系统教程
            new TutorialNavigationCursors(), // 导航光标教程
            new TutorialConditionalActivators(), // 条件激活器教程
        };

        private int m_selectedTab = 0; // 当前选中的选项卡的索引
        private Vector2 m_scrollPosition; // 滚动视图的位置

        [MenuItem("Window/Mythril2D/Tutorials")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow<TutorialsWindow>();
            window.titleContent = new GUIContent("Tutorials");
            window.Show();
        }

        private string GetFormattedTutorialContent(int index)
        {
            ITutorial tutorial = m_tutorials[index];
            string content = string.Empty;
            content += $"<color=#FFC300><size=18><b>{tutorial.Title}</b></size></color>\n"; // 教程标题
            content += $"<i>{tutorial.Description}</i>\n"; // 教程描述
            foreach (var paragraph in tutorial.Paragraphs)
            {
                content += $"\n{paragraph}\n"; // 教程段落
            }

            return content;
        }

        private void OnGUI()
        {
            minSize = new Vector2(900.0f, 600.0f);

            GUILayout.BeginVertical();

            GUILayout.Label("Learn about Mythril2D by reading our tutorials!", new GUIStyle(EditorStyles.boldLabel)
            {
                richText = true,
                fontSize = 18,
                alignment = TextAnchor.MiddleCenter,
                padding = new RectOffset(5, 5, 5, 5),
                wordWrap = true
            });

            GUILayout.Space(10);

            // 显示选项卡，可以选择不同的教程
            m_selectedTab = GUILayout.SelectionGrid(m_selectedTab,
                m_tutorials.Select(tutorial => tutorial.Title).ToArray(), 6);

            if (m_selectedTab >= 0)
            {
                GUILayout.Space(10);
                m_scrollPosition = GUILayout.BeginScrollView(m_scrollPosition);
                GUIStyle style = new GUIStyle(GUI.skin.box);
                style.stretchWidth = true;
                style.stretchHeight = true;
                style.normal = style.active;
                // 显示选中教程的内容
                GUILayout.Label(GetFormattedTutorialContent(m_selectedTab), new GUIStyle(style)
                {
                    richText = true,
                    padding = new RectOffset(5, 5, 5, 5),
                    margin = new RectOffset(5, 5, 5, 5),
                    fontSize = 14,
                    alignment = TextAnchor.UpperLeft,
                    wordWrap = true
                });

                GUILayout.EndScrollView();
                GUILayout.Space(10);
            }

            GUILayout.EndVertical();
        }
    }
}